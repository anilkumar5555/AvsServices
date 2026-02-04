using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVSTechnoServices.Security
{
    public class CustomPrincipalSerializeModel
    {
        public int UserID { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Password
        { get; set; }
    }
}