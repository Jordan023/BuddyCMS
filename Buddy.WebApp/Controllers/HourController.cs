using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Mvc;
using Buddy.WebApp.Security;
using Buddy.WebAPI.Client;
using Newtonsoft.Json;
using System.Security.Claims;
using Newtonsoft.Json.Linq;
using Buddy.WebApp.Helpers;
using Extensions = Buddy.WebApp.Security.Extensions;
using Buddy.WebApp.Providers;

namespace Buddy.WebApp.Controllers
{
    public class HourController : Controller
    {
        private readonly string _apiUrl = ConfigurationManager.AppSettings["api-url"];

        [Authorization]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void AddHour(string jsonPost)
        {
            if (string.IsNullOrEmpty(_apiUrl))
                throw new Exception("No Api URL configured");

            var userController = new UserController();
            dynamic user = JsonConvert.DeserializeObject((string) userController.GetUserByUsername(User.Identity.Name).Data);
            var userDictionary = new Dictionary<string, dynamic>
            {
                { "user_id",  user.id}
            };

            jsonPost = JsonHelper.AddToJson(jsonPost, userDictionary);
            
            var client = new Client(_apiUrl);

            var result = client.Post(_apiUrl + "api/hour", jsonPost);
        }
        
        [HttpGet]
        public void DeleteHour(int id)
        {
            if (string.IsNullOrEmpty(_apiUrl))
                throw new Exception("No Api URL configured");
            
            var client = new Client(_apiUrl);

            var result = client.Delete(_apiUrl + "api/hour/" + 1);
        }

        [HttpGet]
        public JsonResult GetHourByUserAndDate()
        {
            if (string.IsNullOrEmpty(_apiUrl))
                throw new Exception("No Api URL configured");

            var client = new Client(_apiUrl);

           // var user = UserProvider.GetUser(User.Identity.Name, true);
            var result = client.Get(_apiUrl + "api/hour?QuerySearch=" + User.Identity.Name + "&pageNumber=1&pageSize=1000");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}