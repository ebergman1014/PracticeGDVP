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
