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
        // objects used
        private DiscountService discountService;
        private UserDiscount userDiscount;
        // mocks needed
        private Mock<IUserDiscountUtility> mockUserDiscountUtility = new Mock<IUserDiscountUtility>();
        private Mock<IPracticeGDVPDao> mockContext = new Mock<IPracticeGDVPDao>();
        private Mock<IDbSet<UserDiscount>> mockDbset = new Mock<IDbSet<UserDiscount>>();
        private Mock<DiscountService> mockUserDiscount = new Mock<DiscountService>();


        // objects used
        private UserDiscount coupon = User_DiscountTest.CreateCoupon();
        private List<UserDiscount> couponList;
        // isSuccess is error 
        private bool isSuccess;
        private String error;
        // CONST used to rid of magic #'s
        private const int USER4 = 4;
        private const String DISCOUNTCODE4 = "ABCDE";

        [TestInitialize]
        public void Setup()
        {
            couponList = new List<UserDiscount>();
            discountService = (DiscountService)DiscountService.GetInstance();
            userDiscount = User_DiscountTest.CreateCoupon();
            couponList.Add(coupon);
            isSuccess = false;
            error = null;
            discountService.couponUtility = mockUserDiscountUtility.Object;
            discountService.dbContext = mockContext.Object;

        }


        [TestMethod]
        public void DiscountServiceCreateCouponSuccessTest()
        {

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
            // used mockUserDiscoutn for virtual method, to access lambda expression
            mockUserDiscount.Setup(m => m.GetCouponList(coupon)).Returns(couponList);
            Assert.AreSame(coupon, mockUserDiscount.Object.RedeemCoupon(coupon, out isSuccess));
            Assert.IsTrue(isSuccess);
            Assert.IsTrue(coupon.Reedemed);
        }
    }
}
