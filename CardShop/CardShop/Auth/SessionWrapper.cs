using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CardShop.Auth
{
    public class SessionWrapper : ISession 
    {
        private HttpSessionState session;

        public SessionWrapper(HttpSessionState session)
        {
            this.session = session;
        }

        public object this[int index]
        {
            get
            {
               return session[index];
            }
            set
            {
                session[index] = value;
            }
        }

        public object this[string name]
        {
            get
            {
                return session[name];
            }
            set
            {
                session[name] = value;
            }
        }
    }
}