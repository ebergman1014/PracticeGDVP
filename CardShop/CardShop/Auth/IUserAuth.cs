using CardShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace CardShop.Auth
{
    public interface IUserAuth
    {
        User User { get; set; }
        User ActingAs { get; set; }
        User ActingUser { get; }

        void Logout();

        bool HasRole(params Role[] roles);

        bool IsLoggedIn();

        bool Login(IPrincipal principal);
    }
}
