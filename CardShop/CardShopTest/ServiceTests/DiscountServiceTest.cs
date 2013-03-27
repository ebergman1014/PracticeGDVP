using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Service;
using CardShop.Utilities;
using CardShop.Daos;
using CardShop.Models;
using CardShopTest.TestHelper;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Linq;


namespace CardShopTest.ServiceTests
{
    [TestClass]
    public class DiscountServiceTest
    {
        private DiscountService discountService;
        private UserDiscount userDiscount;
        private Mock<IUserDiscountUtility> mockUserDiscountUtility = new Mock<IUserDiscountUtility>();
        private Mock<IPracticeGDVPDao> mockContext = new Mock<IPracticeGDVPDao>();

        private Mock<IDbSet<UserDiscount>> mockDbset = new Mock<IDbSet<UserDiscount>>();

        private UserDiscount coupon = User_DiscountTest.CreateCoupon();
        private List<UserDiscount> couponList = new List<UserDiscount>();
        private bool isSuccess;
        private String error;
        private const int USER4 = 4;
        private const String DISCOUNTCODE4 = "ABCDE";

        [TestInitialize]
        public void Setup()
        {
            discountService = (DiscountService)DiscountService.GetInstance();
            userDiscount = User_DiscountTest.CreateCoupon();
            couponList.Add(coupon);
        }


        [TestMethod]
        public void DiscountServiceCreateCouponSuccessTest()
        {
            discountService.couponUtility = mockUserDiscountUtility.Object;
            discountService.dbContext = mockContext.Object;


            mockUserDiscountUtility.Setup(mock => mock.GenerateCoupon()).
                Returns(userDiscount.DiscountCode);
            mockContext.Setup(mock => mock.UserDiscounts().
                Add(userDiscount)).Returns(userDiscount);

            Assert.AreSame(discountService.CreateCoupon(userDiscount), userDiscount);

            mockContext.Verify(mock => mock.SaveChanges());
        }

        [TestMethod]
        public void DiscountServicesValidateCoupon()
        {

            List<UserDiscount> testList = new List<UserDiscount>();
            testList.Add(userDiscount);

            Assert.AreSame(userDiscount, discountService.ValidateCoupon(testList, out isSuccess, out error));

        }


        [TestMethod]
        public void DiscountServiceReedemCoupon()
        {
            var userDiscount = new Mock<DiscountService>();

            userDiscount.Setup(m => m.GetCouponList(coupon)).Returns(couponList);
            Assert.AreSame(coupon, userDiscount.Object.RedeemCoupon(coupon, out isSuccess));
            Assert.IsTrue(isSuccess);
            Assert.IsTrue(coupon.Reedemed);
        }
    }
}
