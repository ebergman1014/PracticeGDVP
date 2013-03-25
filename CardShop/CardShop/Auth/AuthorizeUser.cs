using CardShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CardShop.Auth
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {

        private Role[] roles;

        public AuthorizeUserAttribute()
        {
            roles = new Role[] { };
        }

        public AuthorizeUserAttribute(params Role[] roles){
            this.roles = roles;
        }
        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = false;
            IUserAuth auth = UserAuth.GetUserAuth(httpContext);
            if (auth.ActingUser != null)
            {
                if (roles.Length > 0)
                {
                    foreach(Role role in roles){
                        if (auth.ActingUser.RoleId == (int)role)
                        {
                            result = true;
                            break;
                        }
                    }
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }
    }
}