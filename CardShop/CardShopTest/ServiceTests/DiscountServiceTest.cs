using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Service;
using CardShop.Utilities;
using CardShop.Daos;
using CardShop.Models;
using CardShopTest.TestHelper;
using Moq;
using System.Collections.Generic;

namespace CardShopTest.ServiceTests
{
    [TestClass]
    public class DiscountServiceTest
    {
        private DiscountService discountService;
        private UserDiscount userDiscount;

        [TestInitialize]
        public void Setup()
        {
            discountService = new DiscountService();
            userDiscount = User_DiscountTest.CreateCoupon();
        }

        [TestMethod]
        public void GetAllUsers()
        {
            var mockContext = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();
            List<User> list = ListOfUsers.GetListOfUsers(4);
            mockContext.Setup(m => m.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(m => m.ToList()).Returns(list);
            discountService.dbContext = mockContext.Object;

            Assert.AreSame(list, discountService.GetAllUsers());
        }

        [TestMethod]
        public void CreateCouponSuccessTest()
        {
            var mockUserDiscountUtility = new Mock<IUserDiscountUtility>();
            var mockDbSet = new Mock<IPracticeGDVPDao>();
            discountService.couponUtility = mockUserDiscountUtility.Object;
            discountService.dbContext = mockDbSet.Object;
            

            mockUserDiscountUtility.Setup(mock => mock.GenerateCoupon()).
                Returns(userDiscount.DiscountCode);
            mockDbSet.Setup(mock => mock.UserDiscounts().
                Add(userDiscount)).Returns(userDiscount);

            Assert.AreSame(discountService.CreateCoupon(userDiscount), userDiscount);

            mockDbSet.Verify(mock => mock.SaveChanges());
        }
    }
}
