using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CardShop.Service;
using CardShop.Controllers;
using CardShop.Models;
using System.Web.Mvc;
using System.Diagnostics;
using CardShopTest.TestHelper;

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
        UserDiscount badCoupon;

        /// <summary>
        /// Setup for each test
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            discountTest = new DiscountController();
            coupon = new UserDiscount();
            badCoupon = User_DiscountTest.CreateCoupon();
            badCoupon.DiscountCode = null;
        }

        /// <summary>
        /// Test the IssueDiscount [HttpPost]
        /// </summary>
        [TestMethod]
        public void IssueDiscountGoodPostTest()
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
        public void IssueDiscountBadPostTest()
        {
            var mock = new Mock<IDiscountService>();
            discountTest.discountService = mock.Object;
            // create coupon
            coupon = User_DiscountTest.CreateCoupon();
            // set DiscountCode to null
            coupon.DiscountCode = null;
            mock.Setup(mockObject => mockObject.CreateCoupon(coupon)).Returns(badCoupon);
            // cast ActionResult to JsonResult, pull data, cast to UserDiscount
            var json = ((JsonResult)discountTest.IssueDiscount(coupon));
            // 
            Assert.IsInstanceOfType(json, typeof(JsonResult));
        }

        [TestMethod]
        public void IssueDiscountBadInputsTest()
        {
            var mock = new Mock<IDiscountService>();
            discountTest.discountService = mock.Object;
            // sets IsValid in the controller to false
            discountTest.ModelState.AddModelError("don't work", "alrights.");
            var json = (JsonResult)discountTest.IssueDiscount(badCoupon);
           // UserDiscount returnedDiscount = (UserDiscount)json.Data;
            // verify the returned object matches object sent in 
            // (if test IsValid was true, returnedDiscount would be null)
            Assert.IsInstanceOfType(json, typeof(JsonResult));
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
