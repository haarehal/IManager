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
    }
}