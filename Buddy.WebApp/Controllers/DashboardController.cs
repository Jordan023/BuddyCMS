using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Buddy.WebApp.Security;
using Buddy.WebAPI.Client;

namespace Buddy.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        [Authorization]
        public ActionResult Index()
        {
            if (Session["api-url"] == null)
                return Redirect("~/Login/");

            var _client = new Client(Session["api-url"].ToString());

            if (_client != null)
            {
                if (!string.IsNullOrEmpty((string)Session["access-token"]))
                {
                    _client.AccessToken = (string)Session["access-token"];
                }
            }
            return View();
        }
    }
}