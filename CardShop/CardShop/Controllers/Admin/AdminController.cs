using CardShop.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardShop.Controllers.Admin
{
    [AuthorizeUser(Role.StoreOwner, Role.Admin)]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

    }
}
