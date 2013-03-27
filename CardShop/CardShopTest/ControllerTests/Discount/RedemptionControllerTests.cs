using System;
using System.Web.Mvc;
using CardShop.Controllers;
using CardShop.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CardShop.Models;
using CardShop.Daos;
using CardShopTest.TestHelper;
using System.Data.Entity;

namespace CardShopTest.ControllerTests
{
    /// <summary>
    /// Test Class for RedemptionController
    /// </summary>
    /// 
    [TestClass]
    public class RedemptionControllerTests
    {
        //  fields
        Mock<IDiscountService> discountService = new Mock<IDiscountService>();
        Mock<IDbSet<User>> idbSetUser = new Mock<IDbSet<User>>();
        private RedemptionController redemptionController;
        private const int USER4 = 4;
        private const string COUPONCODE4 = "ABCDE";
        private bool isSuccess;
        private string error;

        private UserDiscount coupon = User_DiscountTest.CreateCoupon();
        /// <summary>
        /// Setup for each test
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            redemptionController = new RedemptionController();
            redemptionController.discountService = discountService.Object;

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void RedemptionIndexTest()
        {
            Assert.IsInstanceOfType(redemptionController.Index(), typeof(RedirectToRouteResult)); 
        }

        [TestMethod]
        public void RedeemTest()
        {
            redemptionController.discountService = discountService.Object;

            //discountService.Setup(m => m.GetAllUsers()).Returns(idbSetUser.Object.ToList);

            Assert.IsInstanceOfType(redemptionController.Redeem(), typeof(ViewResult));
            discountService.Verify(m => m.GetAllUsers());

        }

        [TestMethod]
        public void RedeemDiscountFailTest()
        {
            discountService.Setup(m => m.GetCoupon(USER4, COUPONCODE4, out isSuccess, out error)).Returns((UserDiscount)null);

            Assert.IsInstanceOfType(redemptionController.RedeemDiscount(USER4, COUPONCODE4), typeof(JsonResult));

        }

        [TestMethod]
        public void RedeemDiscountPassTest()
        {
            discountService.Setup(m => m.GetCoupon(USER4, COUPONCODE4, out isSuccess, out error)).Returns(coupon);
            discountService.Setup(m => m.RedeemCoupon(coupon, out isSuccess)).Returns(coupon);

            Assert.IsInstanceOfType(redemptionController.RedeemDiscount(USER4, COUPONCODE4), typeof(JsonResult));
            discountService.Verify(mockObject => mockObject.RedeemCoupon(coupon, out isSuccess));
        }

    }
}
