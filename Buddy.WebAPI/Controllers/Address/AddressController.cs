using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Address;

namespace Buddy.WebAPI.Controllers.Address
{
    public class AddressController : ApiController
    {
        private BuddyContext _context;

        public AddressController()
        {
            _context = new BuddyContext();
        }

        // GET /api/addresses
        public IEnumerable<AddressDto> GetAddresses()
        {
            return _context.Addresses.ToList().Select(Mapper.Map<CodeFirst.Models.Address.Address, AddressDto>);
        }

        // GET /api/addresses/1
        public IHttpActionResult GetAddress(int id)
        {
            var address = _context.Addresses.SingleOrDefault(c => c.Id == id);

            if (address == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Address.Address, AddressDto>(address));
        }

        // POST /api/addresses
        [HttpPost]
        public IHttpActionResult CreateAddress(AddressDto addressDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var address = Mapper.Map<AddressDto, CodeFirst.Models.Address.Address>(addressDto);
            _context.Addresses.Add(address);
            _context.SaveChanges();

            addressDto.Id = address.Id;

            return Created(new Uri(Request.RequestUri + "/" + address.Id), addressDto);
        }

        // PUT /api/addresses/1
        [HttpPut]
        public void UpdateAddress(int id, AddressDto addressDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var addressInDb = _context.Addresses.SingleOrDefault(c => c.Id == id);

            if(addressInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(addressDto, addressInDb);

            _context.SaveChanges();
        }

        // DELETE /api/addresses/1
        [HttpDelete]
        public void DeleteAddress(int id)
        {
            var addressInDb = _context.Addresses.SingleOrDefault(c => c.Id == id);

            if (addressInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Addresses.Remove(addressInDb);
            _context.SaveChanges();
        }
    }
}