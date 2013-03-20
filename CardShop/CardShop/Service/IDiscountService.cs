using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Service
{
    /// <summary>
    /// Interface for DiscountService
    /// </summary>
    public interface IDiscountService
    {
        /// <summary>
        /// Returns a list of all the Users
        /// </summary>
        /// <returns> returns List<CardShop.Models.User> </returns>
        List<User> GetAllUsers();
        
        /// <summary>
        /// Create and save Coupon to DB
        /// </summary>
        /// <param name="coupon">Coupon without coupon code</param>
        /// <returns>Coupon with Coupon Code</returns>
        UserDiscount CreateCoupon(UserDiscount coupon);
    }
}
