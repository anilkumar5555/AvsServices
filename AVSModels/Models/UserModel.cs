using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVSModels.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public  int   CreatedBy { get; set; }
        public  string  ConformPassword { get; set; }
    }
}
