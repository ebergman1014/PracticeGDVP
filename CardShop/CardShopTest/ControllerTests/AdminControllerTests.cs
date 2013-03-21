using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Controllers;
using System.Web.Mvc;
using System.Web.Security;
using CardShop.Models;
using CardShop.Service;
using Moq;
using CardShop.Auth;


using System.Collections.Generic;
using System.Linq;
using System.Text;




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
            var mockMember = new Mock<IMembership>();
            adminController.membership = mockMember.Object;

            mockMember.Setup(m => m.GetUserId()).Returns(2);
            //mock.Setup(m => m.OwnedStore(2)).Returns(null);
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
            var mock = new Mock<IMembership>();
            
            mock.Setup(m => m.GetUser()).Returns((MembershipUser)null); //.Raises();

            adminController.membership = mock.Object;

            adminController.ManageStore();
            Assert.IsInstanceOfType(adminController.ManageStore(),typeof(RedirectResult));

        }


    }
}
