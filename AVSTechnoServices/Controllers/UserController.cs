using AVSModels.Models;
using AVSTechnoServices.Security;
using Common.Services.Implementation;
using Common.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace AVSTechnoServices.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private IUserService _userservices;
        private IRoleServices _roleServices;
        public UserController()
        {
            _userservices = new UserService();
            _roleServices = new RoleServices();
        }
        // GET: User
        public async Task<ActionResult>Index()
        {
            var result = await _userservices.getallusers();
            return  View(result);
        }
        [HttpGet]
        public async Task<ActionResult>  GetUser()
        {
            int userid = User.UserID; 
            var result = await _userservices.GetUserbyID(userid);
            return  View(result); 
        }
        [HttpGet]
        public async Task<ActionResult> AddUser(int userid=0)
        {
            UserModel user = new UserModel();
            var roles = await _roleServices.GetRoles();
            ViewBag.roles = roles.Select(a => new { value = a. RoleID, text = a.RoleName });
            if (userid != 0)
            {
                user = await _userservices.GetUserbyID(userid);
            }
            else
            {
                user.UserID = 0;
            }
            return View(user); 
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(UserModel model)
        {
            var result = false; 
            if (ModelState.IsValid)
            {
                model.CreatedBy = User.UserID;
                if (model.UserID != 0)
                    result =  Convert.ToBoolean(await _userservices.UpdateUser(model));
                else
                    result = Convert.ToBoolean(await _userservices.CreateUser(model));
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<ActionResult> DeleteUser(int userid)
        {
            var result = Convert.ToBoolean(await _userservices.deleteUser(userid));
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            UserModel model = new UserModel();
            int userid = User.UserID;
            model.UserID = userid;
            return View(model); 
        }
        [HttpPost]
        public async  Task<ActionResult> ChangePassword(UserModel user)
        {
            if (ModelState.IsValid)
            {
                user.UserID = User.UserID;
                if (user.Password.Equals(user.ConformPassword))
                {
                    var result = await _userservices.Updatepassword(user);
                    if (result != 0)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            ModelState.AddModelError("ConformPassword", "Password does not match confirm password");
            return View(user);
        }
    }
}