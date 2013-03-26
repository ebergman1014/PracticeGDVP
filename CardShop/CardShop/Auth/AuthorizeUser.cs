using CardShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CardShop.Auth
{
    /// <summary>
    /// Used to specify a method or class that requires authentication.
    /// </summary>
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {

        private Role[] roles;

        /// <summary>
        /// Requires the user to be logged in, but not of any particular role.
        /// </summary>
        public AuthorizeUserAttribute()
        {
            roles = new Role[] { };
        }
        /// <summary>
        /// Requires the user to have one of the specified roles.
        /// </summary>
        /// <param name="roles">params Role[]</param>
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
            if (UserAuth.GetUserAuth(WrapperFactory.Factory.Wrap<ContextWrapper, IHttpContext>(filterContext.HttpContext)).IsLoggedIn())
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
            IUserAuth auth = UserAuth.GetUserAuth(WrapperFactory.Factory.Wrap<ContextWrapper,IHttpContext>(httpContext));
            return auth.HasRole(roles);
        }
    }
}