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
        public ActionResult ManageStore(Store storeToChange)
        {
            return Json(adminService.EditDiscount(storeToChange));
        }

        [HttpGet]
        [Authorize]
        public ActionResult ManageStore()
        {
            if (membership.GetUser() == null) {
                return Redirect("~/Account/Login");
            }
            return View(adminService.OwnedStore(Convert.ToInt32(
            Membership.GetUser().ProviderUserKey)));
        }

    }
}
