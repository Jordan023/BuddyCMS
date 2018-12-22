using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.User;
using Buddy.WebAPI.Models;
using Buddy.WebApp.Helpers;
using Newtonsoft.Json;

namespace Buddy.WebAPI.Controllers.User
{
    public class UserController : ApiController
    {
        private BuddyContext _context;

        public UserController()
        {
            _context = new BuddyContext();
        }

        // GET /api/users
        public IEnumerable<UserDto> GetUsers([FromUri] PagingParameterModel pagingParameterModel)
        {
            var source = _context.Users.ToList().Select(Mapper.Map<CodeFirst.Models.User.User, UserDto>);
            if (!string.IsNullOrEmpty(pagingParameterModel.QuerySearch))
            {
                source = source.Where(a => a.UserLogin.UserName.Contains(pagingParameterModel.QuerySearch, StringComparison.OrdinalIgnoreCase));
            }

            // Get's No of Rows Count   
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingParameterModel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingParameterModel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage,
                QuerySearch = string.IsNullOrEmpty(pagingParameterModel.QuerySearch) ?
                              "No Parameter Passed" : pagingParameterModel.QuerySearch
            };

            // Setting Header  
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }

        // GET /api/users/1
        public IHttpActionResult GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(c => c.Id == id);

            if (user == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.User.User, UserDto>(user));
        }

        // POST /api/users
        [HttpPost]
        public IHttpActionResult CreateUser(UserDto userDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var user = Mapper.Map<UserDto, CodeFirst.Models.User.User>(userDto);
            _context.Users.Add(user);
            _context.SaveChanges();

            userDto.Id = user.Id;

            return Created(new Uri(Request.RequestUri + "/" + user.Id), userDto);
        }

        // PUT /api/users/1
        [HttpPut]
        public void UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var userInDb = _context.Users.SingleOrDefault(c => c.Id == id);

            if(userInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(userDto, userInDb);

            _context.SaveChanges();
        }

        // DELETE /api/users/1
        [HttpDelete]
        public void DeleteUser(int id)
        {
            var userInDb = _context.Users.SingleOrDefault(c => c.Id == id);

            if (userInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Users.Remove(userInDb);
            _context.SaveChanges();
        }
    }
}