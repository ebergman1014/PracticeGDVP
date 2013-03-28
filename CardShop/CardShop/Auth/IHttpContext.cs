using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace CardShop.Auth
{
    public interface IHttpContext
    {
        ISession Session { get; set; }

        IPrincipal User { get; set; }
    }
}
