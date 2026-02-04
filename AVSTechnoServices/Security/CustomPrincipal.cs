using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AVSTechnoServices.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity
        { get; private set; }

        public bool IsInRole(string role)
        {
            if (!string.IsNullOrEmpty(role) && RolesName.Equals(role))
            { return true; }
            else
            { return false; }
            //if (RolesName.Any(r => role.Contains(r)))
            //{ return true; }
            //else
            //{ return false; }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserID { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public string RolesName { get; set; }

        //public string UserDisplayName { get { return this.UserFirstName + " " + this.UserLastName; } }
    }
}