using AutoMapper;
using InvoiceManager.Dtos;
using InvoiceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoiceManager.Controllers.Api
{
    public class InvoicesController : ApiController
    {
        private ApplicationDbContext _context;

        public InvoicesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/invoices
        public IHttpActionResult GetInvoices(string query = null)
        {
            var invoicesQuery = _context.Invoices.Include(i => i.Items);

            if (!String.IsNullOrWhiteSpace(query)) invoicesQuery = invoicesQuery.Where(i => i.Recipient.Contains(query));

            var invoiceDtos = invoicesQuery.ToList().Select(Mapper.Map<Invoice, InvoiceDto>);

            return Ok(invoiceDtos);
        }

        // GET /api/invoices/{id}
        public IHttpActionResult GetInvoice(int id)
        {
            var invoice = _context.Invoices.SingleOrDefault(i => i.Id == id);

            if(invoice == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Invoice, InvoiceDto>(invoice));
        }

        // POST /api/invoices
        [HttpPost]
        public IHttpActionResult CreateInvoice(InvoiceDto invoiceDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var invoice = Mapper.Map<InvoiceDto, Invoice>(invoiceDto);

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            invoiceDto.Id = invoice.Id;

            return Created(new Uri(Request.RequestUri + "/" + invoice.Id), invoiceDto);
        }

        // PUT /api/invoices/{id}
        [HttpPut]
        public IHttpActionResult UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var invoiceInDb = _context.Invoices.SingleOrDefault(i => i.Id == id);

            if(invoiceInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(invoiceDto, invoiceInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/invoices/{id}
        [HttpDelete]
        public IHttpActionResult DeleteInvoice(int id)
        {
            var invoiceInDb = _context.Invoices.Include(i => i.Items).SingleOrDefault(i => i.Id == id);

            if (invoiceInDb == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(invoiceInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
