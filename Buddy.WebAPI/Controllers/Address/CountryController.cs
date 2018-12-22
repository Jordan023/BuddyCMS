using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Address;

namespace Buddy.WebAPI.Controllers.Country
{
    public class CountryController : ApiController
    {
        private BuddyContext _context;

        public CountryController()
        {
            _context = new BuddyContext();
        }

        // GET /api/countries
        public IEnumerable<CountryDto> GetCountries()
        {
            return _context.Countries.ToList().Select(Mapper.Map<CodeFirst.Models.Address.Country, CountryDto>);
        }

        // GET /api/countries/1
        public IHttpActionResult GetCountry(int id)
        {
            var address = _context.Countries.SingleOrDefault(c => c.Id == id);

            if (address == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Address.Country, CountryDto>(address));
        }

        // POST /api/countries
        [HttpPost]
        public IHttpActionResult CreateCountry(CountryDto addressDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var address = Mapper.Map<CountryDto, CodeFirst.Models.Address.Country>(addressDto);
            _context.Countries.Add(address);
            _context.SaveChanges();

            addressDto.Id = address.Id;

            return Created(new Uri(Request.RequestUri + "/" + address.Id), addressDto);
        }

        // PUT /api/countries/1
        [HttpPut]
        public void UpdateCountry(int id, CountryDto addressDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var addressInDb = _context.Countries.SingleOrDefault(c => c.Id == id);

            if(addressInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(addressDto, addressInDb);

            _context.SaveChanges();
        }

        // DELETE /api/countries/1
        [HttpDelete]
        public void DeleteCountry(int id)
        {
            var addressInDb = _context.Countries.SingleOrDefault(c => c.Id == id);

            if (addressInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Countries.Remove(addressInDb);
            _context.SaveChanges();
        }
    }
}