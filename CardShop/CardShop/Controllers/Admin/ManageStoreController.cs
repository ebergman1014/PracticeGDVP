using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Service;
using CardShop.Models;
using System.Web.Security;
using CardShop.Auth;
using Microsoft.Practices.Unity;
namespace CardShop.Controllers
{
    [AuthorizeUser(Role.StoreOwner, Role.Admin)]
    public class ManageStoreController : Controller, IManageStoreController
    {
        [Dependency]
        public IManageStoreService adminService { get; set; }

        public ManageStoreController(){
        }

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View(adminService.OwnedStore(UserAuth.Current.ActingUser.UserId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Store storeToChange)
        {
            bool success;
            Store store = adminService.EditStore(adminService.OwnedStore(UserAuth.Current.ActingUser.UserId), storeToChange, out success);
            return Json(new {DiscountRate = store.DiscountRate, Name = store.Name, StoreId = store.StoreId, Success = success});
        }

    }
}
