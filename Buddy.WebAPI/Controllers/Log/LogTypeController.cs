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
    public class LogTypeController : ApiController
    {
        private BuddyContext _context;

        public LogTypeController()
        {
            _context = new BuddyContext();
        }

        // GET /api/logs
        public IEnumerable<LogTypeDto> GetLogTypes()
        {
            return _context.LogTypes.ToList().Select(Mapper.Map<CodeFirst.Models.Log.LogType, LogTypeDto>);
        }

        // GET /api/log_types/1
        public IHttpActionResult GetLogType(int id)
        {
            var logType = _context.LogTypes.SingleOrDefault(c => c.Id == id);

            if (logType == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Log.LogType, LogTypeDto>(logType));
        }

        // POST /api/log_types
        [HttpPost]
        public IHttpActionResult CreateLogType(LogTypeDto logTypeDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var logType = Mapper.Map<LogTypeDto, CodeFirst.Models.Log.LogType>(logTypeDto);
            _context.LogTypes.Add(logType);
            _context.SaveChanges();

            logTypeDto.Id = logType.Id;

            return Created(new Uri(Request.RequestUri + "/" + logType.Id), logTypeDto);
        }

        // PUT /api/log_types/1
        [HttpPut]
        public void UpdateLogType(int id, LogDto logDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var logTypeInDb = _context.LogTypes.SingleOrDefault(c => c.Id == id);

            if(logTypeInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(logDto, logTypeInDb);

            _context.SaveChanges();
        }

        // DELETE /api/log_types/1
        [HttpDelete]
        public void DeleteLogType(int id)
        {
            var logTypeInDb = _context.LogTypes.SingleOrDefault(c => c.Id == id);

            if (logTypeInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.LogTypes.Remove(logTypeInDb);
            _context.SaveChanges();
        }
    }
}