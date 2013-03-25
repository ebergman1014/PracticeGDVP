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
        /// <summary>
        /// If the user is logged in, a 403 view will be rendered. If the user is not logged in, the
        /// action will be passed onto the default handler (asking them to login).
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (UserAuth.GetUserAuth(filterContext.HttpContext).IsLoggedIn())
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Errdocs/403.cshtml"
                };
                filterContext.HttpContext.Response.StatusCode = 403;
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
        
        /// <summary>
        /// Will check to see if the currently-acting user has any of the provided roles.
        /// Otherwise, if no roles are specified, the check will merely be to see if the user is
        /// logged in.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>bool authenticationSuccess</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            IUserAuth auth = UserAuth.GetUserAuth(httpContext);
            return auth.HasRole(roles);
        }
    }
}