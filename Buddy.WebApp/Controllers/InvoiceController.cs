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

namespace Buddy.WebApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly string _apiUrl = ConfigurationManager.AppSettings["api-url"];

        [Authorization]
        public ActionResult Index()
        {
            return View();
        }

        [Authorization]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetInvoices()
        {
            if (string.IsNullOrEmpty(_apiUrl))
                throw new Exception("No Api URL configured");

            var client = new Client(_apiUrl);

            var result = client.Get(_apiUrl + "api/invoice");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}