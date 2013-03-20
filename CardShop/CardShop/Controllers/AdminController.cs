using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Service;
using CardShop.Models;
using System.Web.Security;

namespace CardShop.Controllers
{
    public class AdminController : Controller, IAdminController
    {
        public IAdminService adminService {get; set;}
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ManageStore(Store storeToChange)
        {
            return Json(adminService.EditDiscount(storeToChange));
        }

        [HttpGet]
        public ActionResult ManageStore()
        {
            return View(adminService.OwnedStore(Membership.GetUser().ProviderUserKey));
        }

    }
}
