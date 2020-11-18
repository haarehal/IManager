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
                Items = items
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

            double subtotal = 0;

            foreach (Item item in viewModel.Items)
            {
                item.Amount = item.Quantity * item.UnitPrice;
                subtotal += item.Amount;
            }

            viewModel.Invoice.Subtotal = subtotal;
            viewModel.Invoice.Total = Math.Round(((viewModel.Invoice.Tax * subtotal) / 100) + subtotal , 2);
            viewModel.Invoice.DateCreated = DateTime.Now;
            viewModel.Invoice.Items = viewModel.Items;
            viewModel.Invoice.CreatorId = User.Identity.GetUserId();

            if (viewModel.Invoice.Tax == 17)
            {
                viewModel.Invoice.Currency = CurrencyName.BosnianCurrency;
            }
            else if(viewModel.Invoice.Tax == 25)
            {
                viewModel.Invoice.Currency = CurrencyName.CroatianCurrency;
            }

            _context.Invoices.Add(viewModel.Invoice);
            _context.SaveChanges();

            return RedirectToAction("List", "Invoices");
        }
    }
}
