using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Models;
using CardShop.Utilities;
using CardShop.Daos;

namespace CardShop.Service
{

    /// <summary>
    /// DiscountService class will handle all service type requests
    /// </summary>
    public class DiscountService : IDiscountService
    {
        /// <summary>
        /// CouponUtility
        /// </summary>
        public IUserDiscountUtility couponUtility { get; set; }
        public IPracticeGDVPDao dbContext { get; set; }

        /// <summary>
        /// GetAllUsers gets a list of all the Users
        /// </summary>
        /// <returns> a list of all the users in the DB</returns>
        public List<User> GetAllUsers()
        {
            List<User> users;
            using (var ctx = dbContext)
            {
                //get all users to a list
                users = ctx.Users().ToList();
            }
            return users;
        }
        /// <summary>
        /// Create and submit coupon
        /// </summary>
        /// <param name="coupon">Coupon without code</param>
        /// <returns>Coupon with code</returns>
        public UserDiscount CreateCoupon(UserDiscount coupon)
        {
            // generate five digit coupon code
            if (coupon.StartDate < coupon.EndDate &&
                (coupon.DiscountRate > 0 && coupon.DiscountRate < 100))
            {
                coupon.DiscountCode = couponUtility.GenerateCoupon();
                using (var ctx = new PracticeGDVPEntities())
                {
                    // add coupon to context
                    var userCoupons = ctx.UserDiscounts.Add(coupon);
                    // save changes to context (saves to DB!)
                    ctx.SaveChanges();
                }
            }
            return coupon;
        }
        /// <summary>
        /// No-Args constructor, creates new CouponUtility();
        /// </summary>
        public DiscountService()
        {
            couponUtility = new UserDiscountUtility();
            dbContext = PracticeGDVPDao.GetInstance();
        }
    }
}