﻿using System;
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
        /// Get Coupon by userId and discountCode
        /// returns null if no UserDiscount is found
        /// Using outs to return additional error information
        /// Also checks for expiration and redemption status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="DiscountCode"></param>
        /// <returns>UserDiscount</returns>
        /// <author>Paul Wroe</author>
        public UserDiscount GetCoupon(int userId, String discountCode, out bool isSuccess, out String error)
        {
            error = "none";
            isSuccess = false;
            UserDiscount returnCoupon = null;
            
            using (var ctx = dbContext)
            {
                //  get coupon by id and coupon code
                //  LINQ query instead of Lambda expression for clarity
                var coupon = from cup 
                             in ctx.UserDiscounts()
                             where
                             cup.DiscountCode == discountCode
                             &&
                             cup.UserId == userId
                             select cup;

                //  check for exists, expired, or used
                if (coupon.ToList().Count > 0)
                {
                    returnCoupon = coupon.ToList().First(); //  fails if sequence contains no elements
                    if (returnCoupon.Reedemed)  //  false is not redeemed
                    {
                        error = "Coupon already redeemed.";
                    }
                    else if (DateTime.Compare(returnCoupon.EndDate, DateTime.Now) < 0)   // coupon is expired
                    {
                        error = "Coupon is expired.";
                    }
                    else if (DateTime.Compare(returnCoupon.StartDate, DateTime.Now) > 0)  //  coupon is not yet active
                    {
                        error = "Coupon is not yet active.  Coupon starts " + returnCoupon.StartDate;
                    }
                    else
                    {
                        isSuccess = true;
                    }
                }
                else
                {
                    error = "Unable to find Coupon.";
                }
            }

            return returnCoupon;
        }

        /// <summary>
        /// Mark Inputted coupon as redeemed.
        /// </summary>
        /// <returns>UserDisount</returns>
        /// <author>Paul Wroe</author>
        public UserDiscount RedeemCoupon(UserDiscount coupon, out bool isSuccess)
        {
            isSuccess = false;

            coupon.Reedemed = true;
            using (var ctx = dbContext)
            {
                // add coupon to context
                var userCoupon = ctx.UserDiscounts().Where(p => p.UserDiscountId == coupon.UserDiscountId).FirstOrDefault();
                userCoupon.Reedemed = true;
                ctx.SaveChanges();
                isSuccess = true;
            }

            return coupon;
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
                using (var ctx = dbContext)
                {
                    // add coupon to context
                    var userCoupons = ctx.UserDiscounts().Add(coupon);
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