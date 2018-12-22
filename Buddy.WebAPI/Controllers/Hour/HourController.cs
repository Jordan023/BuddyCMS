using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.WebAPI.Dtos.Hour;
using Buddy.WebAPI.Models;
using Buddy.WebApp.Helpers;
using Newtonsoft.Json;

namespace Buddy.WebAPI.Controllers.Hour
{
    public class HourController : ApiController
    {
        private BuddyContext _context;

        public HourController()
        {
            _context = new BuddyContext();
        }

        // GET /api/hours
        public IEnumerable<HourDto> GetHours([FromUri] PagingParameterModel pagingParameterModel)
        {
            var source = _context.Hours.ToList().Select(Mapper.Map<CodeFirst.Models.Hour.Hour, HourDto>);
            if (!string.IsNullOrEmpty(pagingParameterModel.QuerySearch))
            {
                source = source.Where(a => a.User.UserLogin.UserName.Contains(pagingParameterModel.QuerySearch, StringComparison.OrdinalIgnoreCase));
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

        // GET /api/hours/1
        public IHttpActionResult GetHour(int id)
        {
            var hour = _context.Hours.SingleOrDefault(c => c.Id == id);

            if (hour == null)
                return NotFound();

            return Ok(Mapper.Map<CodeFirst.Models.Hour.Hour, HourDto>(hour));
        }

        // POST /api/hours
        [HttpPost]
        public IHttpActionResult CreateHour(HourDto hourDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var hour = Mapper.Map<HourDto, CodeFirst.Models.Hour.Hour>(hourDto);
            _context.Hours.Add(hour);
            _context.SaveChanges();

            hourDto.Id = hour.Id;

            return Created(new Uri(Request.RequestUri + "/" + hour.Id), hourDto);
        }

        // PUT /api/hours/1
        [HttpPut]
        public void UpdateHour(int id, HourDto hourDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var hourInDb = _context.Hours.SingleOrDefault(c => c.Id == id);

            if(hourInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(hourDto, hourInDb);

            _context.SaveChanges();
        }

        // DELETE /api/hours/1
        [HttpDelete]
        public void DeleteHour(int id)
        {
            var hourInDb = _context.Hours.SingleOrDefault(c => c.Id == id);

            if (hourInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Hours.Remove(hourInDb);
            _context.SaveChanges();
        }
    }
}