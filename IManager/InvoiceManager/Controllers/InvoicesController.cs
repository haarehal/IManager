using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InvoiceManager.Models;
using System.Data.Entity;
using InvoiceManager.ViewModels;
using System.Data.Entity.Validation;
using System.Text;
using Microsoft.AspNet.Identity;

namespace InvoiceManager.Controllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext _context;

        public InvoicesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return RedirectToAction("List", "Invoices");
        }

        public ActionResult List()
        {
            var invoices = _context.Invoices.Include(i => i.Items).ToList();

            if (User.IsInRole(RoleName.CanManageInvoices))
            {
                return View("List", invoices);
            }
            else
            {
                return View("ReadOnlyList", invoices);
            }
        }

        public ActionResult Details(int id)
        {
            var invoice = _context.Invoices.Include(i => i.Items).SingleOrDefault(i => i.Id == id);

            return View(invoice);
        }

        [Authorize(Roles = RoleName.CanManageInvoices)]
        public ActionResult New()
        {
            var items = new List<Item>();
            items.Add(new Item());

            var viewModel = new InvoiceFormViewModel
            {
                Invoice = new Invoice(),
                Items = items
            };

            return View("InvoiceForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var invoice = _context.Invoices.Include(i => i.Items).SingleOrDefault(i => i.Id == id);

            if (invoice == null)
                return HttpNotFound();

            var viewModel = new InvoiceFormViewModel
            {
                Invoice = invoice,
                Items = invoice.Items
            };

            return View("InvoiceForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("InvoiceForm", viewModel);
            }

            foreach (Item item in viewModel.Items)
            {
                item.Amount = item.Quantity * item.UnitPrice;
                viewModel.Invoice.Subtotal += item.Amount;
            }

            viewModel.Invoice.Total = Math.Round(((viewModel.Invoice.TaxPercentage * viewModel.Invoice.Subtotal) / 100) + viewModel.Invoice.Subtotal, 2);
            viewModel.Invoice.DateCreated = DateTime.Now;
            viewModel.Invoice.Items = viewModel.Items;
            viewModel.Invoice.CreatorId = User.Identity.GetUserId();

            if (viewModel.Invoice.TaxPercentage == 17)
            {
                viewModel.Invoice.Currency = CurrencyName.BosnianCurrency;
            }
            else if (viewModel.Invoice.TaxPercentage == 25)
            {
                viewModel.Invoice.Currency = CurrencyName.CroatianCurrency;
            }

            _context.Invoices.Add(viewModel.Invoice);
            _context.SaveChanges();

            return RedirectToAction("List", "Invoices");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(InvoiceFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("InvoiceForm", viewModel);
            }

            var invoiceInDb = _context.Invoices.Single(i => i.Id == viewModel.Invoice.Id);

            invoiceInDb.DateFinal = viewModel.Invoice.DateFinal;
            invoiceInDb.Recipient = viewModel.Invoice.Recipient;
            invoiceInDb.TaxPercentage = viewModel.Invoice.TaxPercentage;

            if (invoiceInDb.TaxPercentage == 17)
            {
                invoiceInDb.Currency = CurrencyName.BosnianCurrency;
            }
            else if (viewModel.Invoice.TaxPercentage == 25)
            {
                invoiceInDb.Currency = CurrencyName.CroatianCurrency;
            }

            foreach (Item item in viewModel.Items)
            {
                if (item.Id == 0) // added new item(s)
                {
                    item.Amount = item.Quantity * item.UnitPrice;
                    item.InvoiceId = viewModel.Invoice.Id;
                    invoiceInDb.Subtotal += item.Amount;

                    _context.Items.Add(item);
                }
                else // updated existing item(s)
                {
                    var itemInDb = _context.Items.FirstOrDefault(i => i.Id == item.Id);

                    invoiceInDb.Subtotal -= itemInDb.Amount;
                    itemInDb.Description = item.Description;
                    itemInDb.Quantity = item.Quantity;
                    itemInDb.UnitPrice = item.UnitPrice;
                    itemInDb.Amount = itemInDb.Quantity * itemInDb.UnitPrice;
                    invoiceInDb.Subtotal += itemInDb.Amount;
                }
            }

            // deleted item(s)
            var itemsDeleted = _context.Items.Where(itemInDb => itemInDb.InvoiceId == viewModel.Invoice.Id).ToList().Where(itemInDb => viewModel.Items.All(itemInForm => itemInForm.Id != itemInDb.Id)).ToList();
            foreach (Item item in itemsDeleted)
            {
                invoiceInDb.Subtotal -= item.Amount;
                _context.Items.Remove(item);
            }

            invoiceInDb.Total = Math.Round(((invoiceInDb.TaxPercentage * invoiceInDb.Subtotal) / 100) + invoiceInDb.Subtotal, 2);

            _context.SaveChanges();

            return RedirectToAction("List", "Invoices");
        }
    }
}
