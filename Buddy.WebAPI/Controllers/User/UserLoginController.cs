using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.User;

namespace Buddy.WebAPI.Controllers.UserLogin
{
    public class UserLoginController : ApiController
    {
        private BuddyContext _context;

        public UserLoginController()
        {
            _context = new BuddyContext();
        }

        // GET /api/userLoginLogins
        public IEnumerable<UserLoginDto> GetUserLoginLogins()
        {
            return _context.UserLogins.ToList().Select(Mapper.Map<CodeFirst.Models.User.UserLogin, UserLoginDto>);
        }

        // GET /api/userLoginLogins/1
        public IHttpActionResult GetUserLogin(int id)
        {
            var userLogin = _context.UserLogins.SingleOrDefault(c => c.Id == id);

            if (userLogin == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.User.UserLogin, UserLoginDto>(userLogin));
        }

        // POST /api/userLoginLogins
        [HttpPost]
        public IHttpActionResult CreateUserLogin(UserLoginDto userLoginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userLogin = Mapper.Map<UserLoginDto, CodeFirst.Models.User.UserLogin>(userLoginDto);
            _context.UserLogins.Add(userLogin);
            _context.SaveChanges();

            userLoginDto.Id = userLogin.Id;

            return Created(new Uri(Request.RequestUri + "/" + userLogin.Id), userLoginDto);
        }

        // PUT /api/userLoginLogins/1
        [HttpPut]
        public void UpdateUserLogin(int id, UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var userLoginInDb = _context.UserLogins.SingleOrDefault(c => c.Id == id);

            if(userLoginInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(userLoginDto, userLoginInDb);

            _context.SaveChanges();
        }

        // DELETE /api/userLoginLogins/1
        [HttpDelete]
        public void DeleteUserLogin(int id)
        {
            var userLoginInDb = _context.UserLogins.SingleOrDefault(c => c.Id == id);

            if (userLoginInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.UserLogins.Remove(userLoginInDb);
            _context.SaveChanges();
        }
    }
}