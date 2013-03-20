using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Controllers;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Service;
using Moq;

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

        [TestMethod]
        public void ManageStoreTest() {
            var mock = new Mock<IAdminService>();
            adminController.adminService = mock.Object;
            Assert.IsInstanceOfType(adminController.ManageStore(new Store()), typeof(JsonResult));
        }
    }
}
