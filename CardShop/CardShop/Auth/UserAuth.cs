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

        public static void CreateSession()
        {
            HttpContext.Current.Session.Add("__UserAuth",  new UserAuth());
        }

        public User getActingUser(){
            return ActingAs != null ? ActingAs : User;
        }
        
        public static IUserAuth GetUserAuth()
        {
            return GetUserAuth(new HttpContextWrapper(HttpContext.Current));
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