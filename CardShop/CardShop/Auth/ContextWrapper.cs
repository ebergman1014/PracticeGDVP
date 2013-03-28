using CardShop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Security.Principal;

namespace CardShop.Auth
{
    public class ContextWrapper : IHttpContext
    {
        public ISession Session { get; set; }
        public static IHttpContext Current
        {
            get
            {
                return Factory.Instance.Create<ContextWrapper, IHttpContext>(HttpContext.Current);
            }
        }
        private HttpContext context;
        
        public ContextWrapper(HttpContext context){
            Wrap(context);
        }
        public ContextWrapper(HttpContextBase context)
        {
            Wrap(context.ApplicationInstance.Context);
        }
 
        private void Wrap(HttpContext context)
        {
            Session = Factory.Instance.Create<SessionWrapper, ISession>(context.Session);
            this.context = context;
        }

        public IPrincipal User
        {
            get
            {
                return context.User;
            }
            set
            {
                context.User = value;
            }
        }
    }
}