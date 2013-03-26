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
        /// Returns a UserDiscount from userId and discountCode
        /// </summary>
        /// <returns> returns List<CardShop.Models.User> </returns>
        UserDiscount GetCoupon(int userId, String discountCode, out bool isSuccess);

        /// <summary>
        /// Takes a UserDiscount and marks it as redeemed
        /// </summary>
        /// <returns> returns List<CardShop.Models.User> </returns>
        UserDiscount RedeemCoupon(UserDiscount coupon, out bool isSuccess);
        
        /// <summary>
        /// Create and save Coupon to DB
        /// </summary>
        /// <param name="coupon">Coupon without coupon code</param>
        /// <returns>Coupon with Coupon Code</returns>
        UserDiscount CreateCoupon(UserDiscount coupon);
    }
}
