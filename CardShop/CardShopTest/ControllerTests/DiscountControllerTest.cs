using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CardShop.Service;
using CardShop.Controllers;
using CardShop.Models;
using System.Web.Mvc;
using System.Diagnostics;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class DiscountControllerTest
    {
        DiscountController discountTest;
        UserDiscount coupon;
        [TestInitialize]
        public void Setup() {
            discountTest = new DiscountController();
            coupon = new UserDiscount();
        }
        [TestMethod]
        public void IssueDiscountPostTest()
        {
        var mock = new Mock<IDiscountService>();
        discountTest.discountService = mock.Object;
        discountTest.IssueDiscount(coupon);
        mock.Verify(m => m.CreateCoupon(coupon));
        }

        [TestMethod]
        public void IssueDiscountGetTest()
        {
            var mock = new Mock<IDiscountService>();
            discountTest.discountService = mock.Object;
            discountTest.IssueDiscount();
            mock.Verify(m => m.GetAllUsers());
        }
        [TestMethod]
        public void IndexGetTest()
        {
            Debug.Write(discountTest.Index().GetType());
            Assert.AreEqual(typeof(ViewResult), (discountTest.Index().GetType()));
        }

    }
}
