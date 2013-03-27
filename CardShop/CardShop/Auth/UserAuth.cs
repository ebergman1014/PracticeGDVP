using CardShop.Models;
using CardShop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardShop.Auth
{
    public class UserAuth : IUserAuth
    {
        /// <summary>
        /// The actual logged-in user.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// The user account that the logged-in user is pretending to be
        /// </summary>
        public User ActingAs { get; set; }
        /// <summary>
        /// Will return the impersonated user, if there is one. Otherwise, will
        /// return the logged-in user.
        /// </summary>
        public User ActingUser{
            get{
                return ActingAs != null ? ActingAs : User;
            }
        }

        /// <summary>
        /// Will return an IUserAuth with the current HttpContext.
        /// </summary>
        public static IUserAuth Current {
            get{
                 return UserAuth.GetUserAuth(Factory.Instance.Create<ContextWrapper,IHttpContext>(HttpContext.Current));
            }
            set
            {
                Current = value;
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
            if (IsLoggedIn() && IsActive())
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

        /// <summary>
        /// Returns whether there is a logged-in user stored in session.
        /// </summary>
        /// <returns>bool</returns>
        public bool IsLoggedIn()
        {
            return (ActingUser != null);
        }

        public bool IsActive()
        {
            return ActingUser.IsActive;
        }

        /// <summary>
        /// Creates a new UserAuth for the current session.
        /// </summary>
        public static void CreateSession()
        {
            HttpContext.Current.Session.Add("__UserAuth",  new UserAuth());
        }

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        public void Logout()
        {

            User = null;
            ActingAs = null;
        }

        /// <summary>
        /// Returns an IUserAuth with the specified HttpContext.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal static IUserAuth GetUserAuth(IHttpContext context)
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