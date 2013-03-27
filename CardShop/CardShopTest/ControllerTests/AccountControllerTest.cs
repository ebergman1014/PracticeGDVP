using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using CardShop.Controllers;
using CardShop.Models;
using CardShop.Filters;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class AccountControllerTests
    {
        AccountController _AccountController;
        InitializeSimpleMembershipAttribute _SimpleMembership;

        [TestInitialize]
        public void TestInitialize()
        {
            _AccountController = (AccountController)ControllerUtility.mockRequest(typeof(AccountController));
            _SimpleMembership = new InitializeSimpleMembershipAttribute();

        }

        [TestCleanup]
        public void TestCleanup()
        {
            _AccountController = null;
        }

        [TestMethod]
        public void LoginGet()
        {
            string returnUrl = "/index";
            ViewResult result = _AccountController.Login(returnUrl) as ViewResult;

            Assert.AreEqual(result.ViewData["ReturnUrl"], returnUrl);
        }

        [TestMethod]
        public void ValidLoginPost()
        {
            string returnUrl = null;
            LoginModel loginModel = new LoginModel();
            loginModel.UserName = "bbob";
            loginModel.Password = "321";
            
            ViewResult result = _AccountController.Login(loginModel, returnUrl) as ViewResult;

            Assert.AreEqual(result.ViewData["ReturnUrl"], returnUrl);
        }
    }
}
