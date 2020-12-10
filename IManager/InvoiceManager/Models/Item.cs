using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Unit price")]
        public double UnitPrice { get; set; }

        public double Amount { get; set; }

        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }
    }
}