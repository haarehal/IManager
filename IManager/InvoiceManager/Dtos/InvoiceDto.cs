﻿using InvoiceManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceManager.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateFinal { get; set; }

        public List<ItemDto> Items { get; set; }

        public double Subtotal { get; set; }

        public double Total { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        [StringLength(20)]
        public string Recipient { get; set; }

        public double TaxPercentage { get; set; }

        public string Currency { get; set; }
    }
}