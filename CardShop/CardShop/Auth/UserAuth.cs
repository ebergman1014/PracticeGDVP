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