using System;
using System.Configuration;
using System.Web.Mvc;
using Buddy.WebAPI.Client;

namespace Buddy.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly string _apiUrl = ConfigurationManager.AppSettings["api-url"];

        [HttpGet]
        public JsonResult GetUserByUsername(string userName)
        {
            if (string.IsNullOrEmpty(_apiUrl))
                throw new Exception("No Api URL configured");

            var client = new Client(_apiUrl);

            var result = client.Get(_apiUrl + "api/user?QuerySearch=" + userName + "&pageNumber=1&pageSize=1");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}