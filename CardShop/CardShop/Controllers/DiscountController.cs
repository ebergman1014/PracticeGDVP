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
    public class DiscountController : Controller, IDiscountController
    {
        public IDiscountService discountService { get; set; }
        /// <summary>
        /// Default page for Discount.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Loads the main page for IssueDiscount
        /// </summary>
        /// <returns> the View of IssueDiscount with AllUsers</returns>
        /// <author>CommanderPaul and masterchief117</author>
        [HttpGet]
        public ActionResult IssueDiscount()
        {
            // gets a list of all the users in DB, returns it!
            return View(discountService.GetAllUsers());
        }

        /// <summary>
        /// Ajax call for IssueDiscount
        /// </summary>
        /// <param name="coupon">coupon without coupon code</param>
        /// <returns> full coupon </returns>
        /// <author>CommanderPaul and masterchief117</author>
        [HttpPost]
        public ActionResult IssueDiscount(UserDiscount coupon)
        {
            // returns a coupon, hopefully with created date!
            UserDiscount returnedDiscount;
            if (this.ModelState.IsValid)
            {
                returnedDiscount = discountService.CreateCoupon(coupon);
            }
            else
            {
                returnedDiscount = coupon;
            }
            return Json(returnedDiscount);
        }

        /// <summary>
        /// No-Args constructor, sets discountService
        /// </summary>
        /// <author>CommanderPaul and masterchief117</author>
        public DiscountController()
        {
            discountService = new DiscountService();
        }
    }
}
