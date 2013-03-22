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
    public class AdminController : Controller, IAdminController
    {
        public IAdminService adminService { get; set; }
        public IMembership membership { get; set; }

        public AdminController(){
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
            Store store = adminService.EditStore(adminService.OwnedStore(membership.GetUserId()), storeToChange);
            return Json(new {DiscountRate = store.DiscountRate, Name = store.Name, StoreId = store.StoreId});
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
