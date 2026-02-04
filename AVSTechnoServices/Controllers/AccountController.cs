using AVSModels.Models;
using AVSTechnoServices.Models;
using AVSTechnoServices.Security;
using Common.Services.Implementation;
using Common.Services.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AVSTechnoServices.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private CustomPrincipalSerializeModel _user;

        public AccountController()
        {
            _userService = new UserService();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Login")]
        public ActionResult Login(string ReturnUrl)
        {
            LoginModel mode = new LoginModel();
            if (string.IsNullOrEmpty(ReturnUrl) && Request.UrlReferrer != null)
                ReturnUrl = Server.UrlEncode(Request.UrlReferrer.PathAndQuery);
            if (Url.IsLocalUrl(ReturnUrl) && !string.IsNullOrEmpty(ReturnUrl))
            {
                ViewBag.ReturnURL = ReturnUrl;
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel loginModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var IsValid = await ValidateUser(loginModel);
                if (IsValid)
                {
                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        decodedUrl = Server.UrlDecode(ReturnUrl);
                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Provided username or password incorrect.");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            UserModel _user = new UserModel();
            return View(_user); 
        }
        [HttpPost]
        public async Task<ActionResult> ForgetPassword(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.GetuserByPhoneNumber(user.PhoneNumber);
                if (result != null)
                {
                    result.Password = string.Empty;
                    return View("ChangePassword", result);
                }
            }
            ModelState.AddModelError("PhoneNumber", "PhoneNumber is invalid");
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword(string phoneNumber=null)
        {
            UserModel model = new UserModel();
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                model.PhoneNumber = phoneNumber;
            }
            return View(model); 
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password.Equals(user.ConformPassword))
                {
                    var result = await _userService.Updatepassword(user);
                    if (result !=  0)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            ModelState.AddModelError("ConformPassword", "Password does not match confirm password");
            return View(user);
        }

        private async Task<bool> ValidateUser(LoginModel loginModel)
        {
            try
            {
                var userdetails = await _userService.GetUser(loginModel.username, loginModel.password);
                if (userdetails != null)
                {
                    if (userdetails.UserID != 0)
                    {
                        _user = new CustomPrincipalSerializeModel
                        {
                            Password = userdetails.Password,
                            FirstName = userdetails.FirstName,
                            LastName = userdetails.LastName,
                            MobileNo = userdetails.PhoneNumber,
                            EmailId = userdetails.Email,
                            RoleId = userdetails.RoleID ?? 0,
                            RoleName = userdetails.RoleName,
                            UserName = userdetails.UserName,
                            UserID = userdetails.UserID
                        };
                        UserPrinciple principle = new UserPrinciple(_user);
                        string userData = JsonConvert.SerializeObject(_user);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, _user.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        false, //pass here true, if you want to implement remember me functionality
                        userData);
                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);
                        return true;
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Provided username or password incorrect.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Provided username or password incorrect.");
                }
                return false;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Password", "Provided username or password incorrect.");
                return false;
            }
        }
        [NoCacheAttribute]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1000;
            Response.CacheControl = "no-cache";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}