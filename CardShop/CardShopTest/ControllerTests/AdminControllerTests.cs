using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Controllers;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Service;
using Moq;
using CardShop.Auth;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class AdminControllerTests
    {
        AdminController adminController;

        [TestInitialize]
        public void Setup() { 
            adminController = new AdminController();
        }

        [TestMethod]
        public void IndexTest()
        {
            Assert.IsInstanceOfType(adminController.Index(), typeof(ActionResult));
        }

        /// <summary>
        /// Test ManageStore Post Method
        /// </summary>
        [TestMethod]
        public void ManageStorePostTest() 
        {
            var mock = new Mock<IAdminService>();

            //set field in adminController
            adminController.adminService = mock.Object;

            Assert.IsInstanceOfType(adminController.
                ManageStore(new Store()), typeof(JsonResult));
        }

        /// <summary>
        /// test ManageStore Get Method
        /// </summary>
        [TestMethod]
        public void ManageStoreGetTest()
        {
            //var mock = new Mock<IMembership>();
            //adminController.membership = mock.Object;

           // mock.Setup(mockObject => mockObject.GetUser()).Callback(null);

           // Assert.IsInstanceOfType(adminController.ManageStore(),typeof(RedirectResult));

        }


    }
}
