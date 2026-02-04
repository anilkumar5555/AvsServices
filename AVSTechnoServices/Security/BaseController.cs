using AVSTechnoServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVSTechnoServices.Security
{
    public class BaseController : Controller
    {
        // GET: Base
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    string url = @"\Account\Login";
        //    if (User == null)
        //    {
        //        filterContext.Result = new RedirectResult(url);
        //        return;
        //    }
        //    else if (!User.Identity.IsAuthenticated)
        //    {
        //        filterContext.Result = new RedirectResult(url);
        //        return;

        //    }
        //}
    }
}