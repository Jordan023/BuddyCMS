using System;
using System.ComponentModel.DataAnnotations;

namespace Buddy.WebAPI.Dtos.Invoice
{
    public class InvoiceProductDto
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public decimal PriceExclVat { get; set; }
        public decimal Vat { get; set; }
        public int Amount { get; set; }
    }
}
