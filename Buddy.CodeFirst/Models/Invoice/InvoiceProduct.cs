using System;
using System.ComponentModel.DataAnnotations;

namespace Buddy.CodeFirst.Models.Invoice
{
    public class InvoiceProduct
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public decimal PriceExclVat { get; set; }
        public decimal Vat { get; set; }
        public int Amount { get; set; }
    }
}
