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
    /// <summary>
    /// Test Class for DiscountController
    /// </summary>
    [TestClass]
    public class DiscountControllerTest
    {
        // objects needed for tests
        DiscountController discountTest;
        UserDiscount coupon;

        /// <summary>
        /// Setup for each test
        /// </summary>
        [TestInitialize]
        public void Setup() {
            discountTest = new DiscountController();
            coupon = new UserDiscount();
        }

        /// <summary>
        /// Test the IssueDiscount [HttpPost]
        /// </summary>
        [TestMethod]
        public void IssueDiscountPostTest()
        {
        var mock = new Mock<IDiscountService>();
        // set the mock object to the object of discountTest
        discountTest.discountService = mock.Object;
        // pass in the coupon
        discountTest.IssueDiscount(coupon);
        // verifies!
        mock.Verify(mockedObject => mockedObject.CreateCoupon(coupon));
        }

        [TestMethod]
        public void IssueDiscountGetTest()
        {

            var mock = new Mock<IDiscountService>();
            discountTest.discountService = mock.Object;
            discountTest.IssueDiscount();
            mock.Verify(mockedObject => mockedObject.GetAllUsers());
        }
        [TestMethod]
        public void IndexGetTest()
        {
            Debug.Write(discountTest.Index().GetType());
            Assert.AreEqual(typeof(ViewResult), (discountTest.Index().GetType()));
        }

    }
}
