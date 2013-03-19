using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShopTest.TestHelper;
using CardShop.Utilities;
using CardShop.Models;

namespace CardShopTest
{
    [TestClass]
    public class CouponDiscountTests
    {

        List<User> testListOfUsers;
        int UsersWanted = 10;
        private UserDiscountUtility couponUtility;
        /// <summary>
        /// Setup for tests contained in CouponDiscountTest
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            testListOfUsers = ListOfUsers.GetListOfUsers(UsersWanted);
            couponUtility = new UserDiscountUtility();
        }

        /// <summary>
        /// 
        /// Tests that GetAllUsers has a Count greater than 0;
        /// </summary>
        [TestMethod]
        public void GetAllUsersTest()
        {
            Assert.IsTrue(testListOfUsers.Count > 0);
        }
        /// <summary>
        /// Tests that no User is not null
        /// </summary>
        [TestMethod]
        public void UserFieldsNotNull()
        {
            // Loop through all Users retrieved, check for null on field
            foreach (User user in testListOfUsers)
            {
                Assert.IsNotNull(user.Name);
                Assert.IsNotNull(user.Password);
                Assert.IsNotNull(user.Username);
                Assert.IsNotNull(user.UserId);
                Assert.IsNotNull(user.Role);
            }

        }
        /// <summary>
        /// Tests that a coupon created has no null values
        /// </summary>
        [TestMethod]
        public void CouponCreationTest()
        {
            User_DiscountTest coupon = User_DiscountTest.CreateCoupon();
            Assert.IsNotNull(coupon.Start_Date);
            Assert.IsNotNull(coupon.End_Date);
            Assert.IsNotNull(coupon.User_DiscountID);
            Assert.IsNotNull(coupon.UserID);
            Assert.IsNotNull(coupon.Number_Of_Cards);
            Assert.IsNotNull(coupon.Discount_Code);
            Assert.IsNotNull(coupon.Discount_Rate);
        }
        /// <summary>
        /// Check that code generated is of length 5
        /// and each char is a capital letter
        /// </summary>
        [TestMethod]
        public void CouponCodeTest()
        {
            for (int index = 0; index < 1000; index++)
            {
                couponUtility = new UserDiscountUtility();
                string CouponCode = couponUtility.GenerateCoupon();
                Assert.IsTrue(CouponCode.Length == 5);
                foreach (char letter in CouponCode)
                {
                    Assert.IsTrue(Convert.ToInt32(letter) > 64);
                    Assert.IsTrue(Convert.ToInt32(letter) < 91);
                }
            }
        }

    }
}
