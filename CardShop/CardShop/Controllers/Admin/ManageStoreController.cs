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
    [AuthorizeUser(Role.StoreOwner, Role.Admin)]
    public class ManageStoreController : Controller, IManageStoreController
    {
        public IManageStoreService adminService { get; set; }
        public IMembership membership { get; set; }

        public ManageStoreController(){
                adminService = new ManageStoreService();
                membership = MembershipWrapper.getInstance();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View(adminService.OwnedStore(Convert.ToInt32(
            membership.GetUserId())));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Store storeToChange)
        {
            bool success;
            Store store = adminService.EditStore(adminService.OwnedStore(membership.GetUserId()), storeToChange, out success);
            return Json(new {DiscountRate = store.DiscountRate, Name = store.Name, StoreId = store.StoreId, Success = success});
        }

    }
}
