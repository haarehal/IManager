using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace InvoiceManager.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Display(Name = "Date of creation")]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Final date for payment")]
        public DateTime DateFinal { get; set; }

        public ICollection<Item> Items { get; set; }

        [Display(Name = "Subtotal")]
        public double Subtotal { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        [Display(Name = "Recipient name")]
        [StringLength(20)]
        public string Recipient { get; set; }

        public double Tax { get; set; }

        public string Currency { get; set; }
    }
}