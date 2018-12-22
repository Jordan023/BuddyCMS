using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Module;

namespace Buddy.WebAPI.Controllers.Module
{
    public class ModuleController : ApiController
    {
        private BuddyContext _context;

        public ModuleController()
        {
            _context = new BuddyContext();
        }

        // GET /api/modules
        public IEnumerable<ModuleDto> GetModules()
        {
            return _context.Modules.ToList().Select(Mapper.Map<CodeFirst.Models.Module.Module, ModuleDto>);
        }

        // GET /api/modules/1
        public IHttpActionResult GetModule(int id)
        {
            var module = _context.Modules.SingleOrDefault(c => c.Id == id);

            if (module == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Module.Module, ModuleDto>(module));
        }

        // POST /api/modules
        [HttpPost]
        public IHttpActionResult CreateModule(ModuleDto moduleDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var module = Mapper.Map<ModuleDto, CodeFirst.Models.Module.Module>(moduleDto);
            _context.Modules.Add(module);
            _context.SaveChanges();

            moduleDto.Id = module.Id;

            return Created(new Uri(Request.RequestUri + "/" + module.Id), moduleDto);
        }

        // PUT /api/modules/1
        [HttpPut]
        public void UpdateModule(int id, ModuleDto moduleDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var moduleInDb = _context.Modules.SingleOrDefault(c => c.Id == id);

            if(moduleInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(moduleDto, moduleInDb);

            _context.SaveChanges();
        }

        // DELETE /api/modules/1
        [HttpDelete]
        public void DeleteModule(int id)
        {
            var moduleInDb = _context.Modules.SingleOrDefault(c => c.Id == id);

            if (moduleInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Modules.Remove(moduleInDb);
            _context.SaveChanges();
        }
    }
}