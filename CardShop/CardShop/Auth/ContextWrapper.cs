using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CardShop.Auth
{
    public class ContextWrapper : IHttpContext
    {
        public ISession Session { get; set; }
        public static IHttpContext Current
        {
            get
            {
                return WrapperFactory.Factory.Wrap<ContextWrapper, IHttpContext>(HttpContext.Current);
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
            Session = WrapperFactory.Factory.Wrap<SessionWrapper, ISession>(context.Session);
            this.context = context;
        }
    }
}