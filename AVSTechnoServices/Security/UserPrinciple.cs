using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVSTechnoServices.Security
{
    public class UserPrinciple
    {
        public static UserPrinciple CurrentInstance;
        public static CustomPrincipalSerializeModel _CurrUser;
        public UserPrinciple()
        {
            CurrentInstance = this;
        }

        public UserPrinciple(CustomPrincipalSerializeModel _user)
        {
            CurrentInstance = this;
            _CurrUser = _user;
        }
    }
}