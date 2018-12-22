using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Invoice
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public virtual User.User Customer { get; set; }
        public virtual Address.Address Address { get; set; }
        public virtual List<InvoiceProduct> InvoiceProducts { get; set; }
    }
}
