using CardShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardShop.Auth
{
    public class UserAuth : IUserAuth
    {
        public User User { get; set; }
        public User ActingAs { get; set; }

        public User ActingUser{
            get{
                return ActingAs != null ? ActingAs : User;
            }
        }

        public static IUserAuth Current {
            get{
                 return UserAuth.GetUserAuth(new HttpContextWrapper(HttpContext.Current));
            }
        }

        /// <summary>
        /// Will check to see if the currently-acting user has any of the provided roles.
        /// Otherwise, if no roles are specified, the check will merely be to see if the user is
        /// logged in.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>bool authenticationSuccess</returns>
        public bool HasRole(params Role[] roles)
        {
            bool result = false;
            if (IsLoggedIn())
            {
                if (roles.Length > 0)
                {
                    foreach (Role role in roles)
                    {
                        if (ActingUser.RoleId == (int)role)
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

        public bool IsLoggedIn()
        {
            return (ActingUser != null);
        }

        public static void CreateSession()
        {
            HttpContext.Current.Session.Add("__UserAuth",  new UserAuth());
        }

        public void Logout()
        {

            User = null;
            ActingAs = null;
        }

        internal static IUserAuth GetUserAuth(HttpContextBase context)
        {
            IUserAuth result = null;
            if (context != null)
            {
                result = (IUserAuth)context.Session["__UserAuth"];
            }
            return result;
        }
    }
}