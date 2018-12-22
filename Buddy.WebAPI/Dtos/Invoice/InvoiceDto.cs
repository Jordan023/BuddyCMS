using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Buddy.WebAPI.Dtos.Address;
using Buddy.WebAPI.Dtos.User;

namespace Buddy.WebAPI.Dtos.Invoice { 

    public class InvoiceDto
    {
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public virtual UserDto Customer { get; set; }
        public virtual AddressDto Address { get; set; }
        public virtual List<InvoiceProductDto> InvoiceProducts { get; set; }
    }
}
