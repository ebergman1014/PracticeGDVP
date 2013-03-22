using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CardShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";


            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            System.Diagnostics.Debug.WriteLine("Authenticated: " + User.Identity.IsAuthenticated);
            if (User.Identity.IsAuthenticated)
            {
                System.Diagnostics.Debug.WriteLine(User.Identity.Name);
                System.Diagnostics.Debug.WriteLine(Membership.GetUser().ProviderUserKey);
            }
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
