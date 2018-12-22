using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Invoice;

namespace Buddy.WebAPI.Controllers.Invoice
{
    public class InvoiceController : ApiController
    {
        private BuddyContext _context;

        public InvoiceController()
        {
            _context = new BuddyContext();
        }

        // GET /api/invoices
        public IEnumerable<InvoiceDto> GetInvoices()
        {
            return _context.Invoices.ToList().Select(Mapper.Map<CodeFirst.Models.Invoice.Invoice, InvoiceDto>);
        }

        // GET /api/invoices/1
        public IHttpActionResult GetInvoice(int id)
        {
            var invoice = _context.Invoices.SingleOrDefault(c => c.Id == id);

            if (invoice == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Invoice.Invoice, InvoiceDto>(invoice));
        }

        // POST /api/invoices
        [HttpPost]
        public IHttpActionResult CreateInvoice(InvoiceDto invoiceDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var invoice = Mapper.Map<InvoiceDto, CodeFirst.Models.Invoice.Invoice>(invoiceDto);
            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            invoiceDto.Id = invoice.Id;

            return Created(new Uri(Request.RequestUri + "/" + invoice.Id), invoiceDto);
        }

        // PUT /api/invoices/1
        [HttpPut]
        public void UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var invoiceInDb = _context.Invoices.SingleOrDefault(c => c.Id == id);

            if(invoiceInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(invoiceDto, invoiceInDb);

            _context.SaveChanges();
        }

        // DELETE /api/invoices/1
        [HttpDelete]
        public void DeleteInvoice(int id)
        {
            var invoiceInDb = _context.Invoices.SingleOrDefault(c => c.Id == id);

            if (invoiceInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Invoices.Remove(invoiceInDb);
            _context.SaveChanges();
        }
    }
}