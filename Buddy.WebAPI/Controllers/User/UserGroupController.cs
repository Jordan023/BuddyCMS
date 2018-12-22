using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.User;

namespace Buddy.WebAPI.Controllers.UserGroup
{
    public class UserGroupController : ApiController
    {
        private BuddyContext _context;

        public UserGroupController()
        {
            _context = new BuddyContext();
        }

        // GET /api/userGroupGroups
        public IEnumerable<UserGroupDto> GetUserGroupGroups()
        {
            return _context.UserGroups.ToList().Select(Mapper.Map<CodeFirst.Models.User.UserGroup, UserGroupDto>);
        }

        // GET /api/userGroupGroups/1
        public IHttpActionResult GetUserGroup(int id)
        {
            var userGroup = _context.UserGroups.SingleOrDefault(c => c.Id == id);

            if (userGroup == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.User.UserGroup, UserGroupDto>(userGroup));
        }

        // POST /api/userGroupGroups
        [HttpPost]
        public IHttpActionResult CreateUserGroup(UserGroupDto userGroupDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var userGroup = Mapper.Map<UserGroupDto, CodeFirst.Models.User.UserGroup>(userGroupDto);
            _context.UserGroups.Add(userGroup);
            _context.SaveChanges();

            userGroupDto.Id = userGroup.Id;

            return Created(new Uri(Request.RequestUri + "/" + userGroup.Id), userGroupDto);
        }

        // PUT /api/userGroupGroups/1
        [HttpPut]
        public void UpdateUserGroup(int id, UserGroupDto userGroupDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var userGroupInDb = _context.UserGroups.SingleOrDefault(c => c.Id == id);

            if(userGroupInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(userGroupDto, userGroupInDb);

            _context.SaveChanges();
        }

        // DELETE /api/userGroupGroups/1
        [HttpDelete]
        public void DeleteUserGroup(int id)
        {
            var userGroupInDb = _context.UserGroups.SingleOrDefault(c => c.Id == id);

            if (userGroupInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.UserGroups.Remove(userGroupInDb);
            _context.SaveChanges();
        }
    }
}