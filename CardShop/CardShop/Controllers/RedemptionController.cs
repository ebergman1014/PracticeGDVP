using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CardShop.Controllers
{
    public class RedemptionController : Controller
    {
        //
        // GET: /Redemption/
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Redeem");
        }

        [HttpGet]
        public ActionResult Redeem()
        {
            return View();
        }
       
    }
}
