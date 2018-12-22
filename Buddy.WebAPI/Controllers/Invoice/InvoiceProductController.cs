using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Invoice;

namespace Buddy.WebAPI.Controllers.InvoiceProduct
{
    public class InvoiceProductController : ApiController
    {
        private BuddyContext _context;

        public InvoiceProductController()
        {
            _context = new BuddyContext();
        }

        // GET /api/invoiceProductProducts
        public IEnumerable<InvoiceProductDto> GetInvoiceProductProducts()
        {
            return _context.InvoiceProducts.ToList().Select(Mapper.Map<CodeFirst.Models.Invoice.InvoiceProduct, InvoiceProductDto>);
        }

        // GET /api/invoiceProductProducts/1
        public IHttpActionResult GetInvoiceProduct(int id)
        {
            var invoiceProduct = _context.InvoiceProducts.SingleOrDefault(c => c.Id == id);

            if (invoiceProduct == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Invoice.InvoiceProduct, InvoiceProductDto>(invoiceProduct));
        }

        // POST /api/invoiceProductProducts
        [HttpPost]
        public IHttpActionResult CreateInvoiceProduct(InvoiceProductDto invoiceProductDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var invoiceProduct = Mapper.Map<InvoiceProductDto, CodeFirst.Models.Invoice.InvoiceProduct>(invoiceProductDto);
            _context.InvoiceProducts.Add(invoiceProduct);
            _context.SaveChanges();

            invoiceProductDto.Id = invoiceProduct.Id;

            return Created(new Uri(Request.RequestUri + "/" + invoiceProduct.Id), invoiceProductDto);
        }

        // PUT /api/invoiceProductProducts/1
        [HttpPut]
        public void UpdateInvoiceProduct(int id, InvoiceProductDto invoiceProductDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var invoiceProductInDb = _context.InvoiceProducts.SingleOrDefault(c => c.Id == id);

            if(invoiceProductInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(invoiceProductDto, invoiceProductInDb);

            _context.SaveChanges();
        }

        // DELETE /api/invoiceProductProducts/1
        [HttpDelete]
        public void DeleteInvoiceProduct(int id)
        {
            var invoiceProductInDb = _context.InvoiceProducts.SingleOrDefault(c => c.Id == id);

            if (invoiceProductInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.InvoiceProducts.Remove(invoiceProductInDb);
            _context.SaveChanges();
        }
    }
}