using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Service;
using CardShop.Models;
using System.Web.Security;
using CardShop.Auth;
namespace CardShop.Controllers
{
    public class Admin2Controller : Controller, IAdminController
    {
        public IAdminService adminService { get; set; }
        public IMembership membership { get; set; }

        public Admin2Controller(){
                adminService = new AdminService();
                membership = MembershipWrapper.getInstance();
        }
        
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageStore(Store storeToChange)
        {
            bool success;
            Store store = adminService.EditStore(adminService.OwnedStore(membership.GetUserId()), storeToChange, out success);
            return Json(new {DiscountRate = store.DiscountRate, Name = store.Name, StoreId = store.StoreId, Success = success});
        }

        [HttpGet]
        [Authorize]
        public ActionResult ManageStore()
        {
            if (membership.GetUser() == null) {
                return Redirect("~/Account/Login");
            }
            return View(adminService.OwnedStore(Convert.ToInt32(
            membership.GetUserId())));
        }

    }
}
