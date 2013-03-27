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
        }

        [TestMethod]
        public void DiscountServiceGetAllUsers()
        {
            var mockDbSet = new Mock<IDbSet<User>>();
            List<User> list = ListOfUsers.GetListOfUsers(4);
            mockContext.Setup(m => m.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(m => m.ToList()).Returns(list);
            discountService.dbContext = mockContext.Object;

            Assert.AreSame(list, discountService.GetAllUsers());
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
        public void DiscountServicesGetCouponPassTest()
        {
            couponList.Add(coupon);
            
            mockContext.Setup(m => m.UserDiscounts()).Returns(mockDbset.Object);
            mockDbset.Setup(m => m.Where(It.IsAny<Expression<Func<UserDiscount, bool>>>())).Returns(mockDbset.Object);
            mockDbset.Setup(m => m.ToList()).Returns(couponList);
            
            
           // Assert.AreSame(coupon, discountService.GetCoupon
           //     (USER4, DISCOUNTCODE4, out isSuccess, out error), "test for return object");
           // Assert.IsTrue(isSuccess);
            //Assert.IsNull(error);
        }

        [TestMethod]
        public void DiscountServicesValidateCoupon()
        {

            List<UserDiscount> testList = new List<UserDiscount>();
            testList.Add(userDiscount);

            Assert.AreSame(userDiscount, discountService.ValidateCoupon(testList, out isSuccess, out error));

        }

    }
}
