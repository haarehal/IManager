using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InvoiceManager.Models;

namespace InvoiceManager.ViewModels
{
    public class InvoiceFormViewModel
    {
        public Invoice Invoice { get; set; }
        public List<Item> Items { get; set; }

        public string Title
        {
            get
            {
                return Invoice.Id == 0 ? "New Invoice" : "Edit Invoice";
            }
        }
    }
}