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

        [HttpPost]
        public ActionResult Redeem(UserDiscount userDiscount)
        {
            return View();
        }

        /// <summary>
        /// Method for retrieveing a discount to see if the coupon
        /// code is valid
        /// </summary>
        /// <returns>Returns UserDiscount</returns>
        [HttpPost]
        public ActionResult RetrieveDiscount(int userId, String couponCode)
        {
            bool isSuccess;

            UserDiscount coupon = discountService.GetCoupon(userId, couponCode, out isSuccess);

            if (isSuccess)
            {
                
            }
            

            return Json(new
            {
                UserDiscountId = coupon.UserDiscountId,
                DiscountRate = coupon.DiscountRate,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                DiscountCode = coupon.DiscountCode,
                UserId = coupon.UserId,
            });


        }


       
    }
}
