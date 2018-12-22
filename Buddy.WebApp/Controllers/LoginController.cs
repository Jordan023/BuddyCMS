using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Buddy.Translations;
using Buddy.WebApp.Models;
using Buddy.WebApp.Providers;
using Buddy.WebApp.Security;
using Newtonsoft.Json;
using Google.Authenticator;

namespace Buddy.WebApp.Controllers
{
        public class LoginController : Controller
        {
            public UserProvider MembershipService { get; set; }
            public AuthProvider AuthorizationService { get; set; }

            private string _twoFactorType = null;
            private const string _twoFactorKey = "1ea43078-a4c2-49b8-b0df-5839d66f309e";

            protected override void Initialize(RequestContext requestContext)
            {
                if (MembershipService == null)
                    MembershipService = new UserProvider();
                if (AuthorizationService == null)
                    AuthorizationService = new AuthProvider();

                base.Initialize(requestContext);
            }

            [HttpGet]
            public ActionResult Index()
            {
                var model = new LoginModel();
#if DEBUG
                model.Username = "Jordan";
                model.Password = "spykie55";
#endif
            return View(model);
            }

            [HttpPost]
            [OutputCache(Duration = 0, VaryByParam = "none")]
            public ActionResult Index(LoginModel model, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    model.Username = model.Username.ToLower();

                    try
                    {
                        if (Session["culture"] != null)
                            MembershipService.LanguageCode =
                                ((CultureInfo) Session["culture"]).TwoLetterISOLanguageName;

                        if (MembershipService.ValidateUser(model.Username, model.Password))
                        {
                            var apiKey = ConfigurationManager.AppSettings["api-key"];
                            var apiUrl = ConfigurationManager.AppSettings["api-url"];

                            Session["api-key"] = apiKey;
                            Session["api-url"] = apiUrl;
                            Session["access-token"] = MembershipService.AccessToken;
                            Session["username"] = model.Username;
                            Session["is-authenticated"] = true;
                            Session["remember-me"] = model.RememberMe;

                            User.AccessTokenExpiresOn(MembershipService.AccessTokenExpiresOn);
                            User.AccessToken(MembershipService.AccessToken);

                            if (!string.IsNullOrEmpty(_twoFactorType))
                            {
                                Session["two-factor-type"] = _twoFactorType;
                                return RedirectToAction("TwoFactor", "Login", new {returnUrl});
                            }
                            else
                            {
                                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);

                                return LoginAndGoToUrl(model.Username, model.RememberMe, returnUrl);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //return RedirectToAction("Index", "Logon", new { message = ex.Message });
                        ModelState.AddModelError("", ex.Message);
                    }

                    ModelState.AddModelError("", "Login not correct!");
                }

                return View(model);
            }

            [HttpGet]
            public ActionResult TwoFactor()
            {
                var model = new TwoFactorModel();

                try
                {
                    var isAuthenticated = false;

                    if (Session["is-authenticated"] != null)
                        isAuthenticated = Convert.ToBoolean(Session["is-authenticated"]);


                    if (isAuthenticated)
                    {
                        var username = Session["username"].ToString();

                        model.Username = username;

                        //Two Factor Authentication Setup
                        TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                        string UserUniqueKey = (username + _twoFactorKey);

                        Session["user_unique_key"] = UserUniqueKey;

                        var setupInfo = TwoFacAuth.GenerateSetupCode("FullStack.Foundation", username, UserUniqueKey,
                            300, 300);

                        model.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                        model.SetupCode = setupInfo.ManualEntryKey;
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                catch (Exception ex)
                {
                    //return RedirectToAction("Index", "Logon", new { message = ex.Message });
                    ModelState.AddModelError("", ex.Message);
                }

                return View(model);
            }

            [HttpPost]
            [OutputCache(Duration = 0, VaryByParam = "none")]
            public ActionResult TwoFactor(TwoFactorModel model, string returnUrl)
            {
                if (ModelState.IsValid)
                {
                    model.Username = model.Username.ToLower();

                    try
                    {
                        var token = model.TwoFactorToken;

                        TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                        string UserUniqueKey = Session["user_unique_key"].ToString();

                        bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, token);

                        if (isValid)
                        {
                            Session["is_valid_two_factor_authentication"] = true;

                            return LoginAndGoToUrl(model.Username, Convert.ToBoolean(Session["remember-me"]),
                                returnUrl);
                        }

                        ModelState.AddModelError(string.Empty, Translation.InvalidTwoFactorCode);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Translation.InvalidTwoFactorCode);
                }

                return View(model);
            }

            private ActionResult LoginAndGoToUrl(string username, bool rememberMe, string returnUrl)
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Dashboard");
            }

            [HttpGet]
            public ActionResult LogOff()
            {
                User.LogOut();
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Login");
            }
        }
    }