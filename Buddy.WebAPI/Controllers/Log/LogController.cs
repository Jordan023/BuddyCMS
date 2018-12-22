using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Log;

namespace Buddy.WebAPI.Controllers.Log
{
    public class LogController : ApiController
    {
        private BuddyContext _context;

        public LogController()
        {
            _context = new BuddyContext();
        }

        // GET /api/logs
        public IEnumerable<LogDto> GetLogs()
        {
            return _context.Logs.ToList().Select(Mapper.Map<CodeFirst.Models.Log.Log, LogDto>);
        }

        // GET /api/logs/1
        public IHttpActionResult GetLog(int id)
        {
            var log = _context.Logs.SingleOrDefault(c => c.Id == id);

            if (log == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Log.Log, LogDto>(log));
        }

        // POST /api/logs
        [HttpPost]
        public IHttpActionResult CreateLog(LogDto logDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var log = Mapper.Map<LogDto, CodeFirst.Models.Log.Log>(logDto);
            _context.Logs.Add(log);
            _context.SaveChanges();

            logDto.Id = log.Id;

            return Created(new Uri(Request.RequestUri + "/" + log.Id), logDto);
        }

        // PUT /api/logs/1
        [HttpPut]
        public void UpdateLog(int id, LogDto logDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var logInDb = _context.Logs.SingleOrDefault(c => c.Id == id);

            if(logInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(logDto, logInDb);

            _context.SaveChanges();
        }

        // DELETE /api/logs/1
        [HttpDelete]
        public void DeleteLog(int id)
        {
            var logInDb = _context.Logs.SingleOrDefault(c => c.Id == id);

            if (logInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Logs.Remove(logInDb);
            _context.SaveChanges();
        }
    }
}