using InvoiceManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceManager.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double Amount { get; set; }

        public int? InvoiceId { get; set; }

        public InvoiceDto Invoice { get; set; }
    }
}