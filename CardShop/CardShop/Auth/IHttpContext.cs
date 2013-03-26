using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Auth
{
    public interface IHttpContext
    {
        ISession Session { get; set; }
    }
}
