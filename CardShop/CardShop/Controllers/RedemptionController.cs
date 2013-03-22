using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Service;
using CardShop.Models;
using System.Web.Services;

namespace CardShop.Controllers
{
    public class RedemptionController : Controller
    {
        public IDiscountService discountService { get; set; }

        public RedemptionController()
        {
            this.discountService = new DiscountService();
        }

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
            ViewBag.listOfUsers = discountService.GetAllUsers().ToList();
            
            return View();
        }
       
    }
}
