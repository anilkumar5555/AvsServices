using AVSTechnoServices.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace AVSTechnoServices
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal _socUser = new CustomPrincipal(authTicket.Name);
                _socUser.UserID = serializeModel.UserID;
                _socUser.FirstName = serializeModel.FirstName;
                _socUser.LastName = serializeModel.LastName;
                _socUser.UserName = serializeModel.UserName;
                _socUser.RoleId = serializeModel.RoleId;
                _socUser.RolesName = serializeModel.RoleName;
                _socUser.RoleName = serializeModel.RoleName;
                _socUser.EmailId = serializeModel.EmailId;
                _socUser.Password = serializeModel.Password;
                HttpContext.Current.User = _socUser;
            }
        }
    }
}
