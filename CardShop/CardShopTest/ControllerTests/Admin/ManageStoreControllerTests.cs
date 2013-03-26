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
using CardShopTest.TestHelper;
using CardShop.Controllers.Admin;


namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class ManageStoreControllerTests
    {
        ManageStoreController manageStoreController;
        List<Store> stores;
        Store storeOne;
        Store storeTwo;

        [TestInitialize]
        public void Setup() { 
            manageStoreController = new ManageStoreController();
            stores = StoreTest.CreateStores(2);
            storeOne = stores[0];
            storeTwo = stores[1];
        }

        [TestMethod]
        public void ManageStoreIndexTest()
        {
            var mock = new Mock<IManageStoreService>();
            var mockMember = new Mock<IMembership>();

            manageStoreController.adminService = mock.Object;
            manageStoreController.membership = mockMember.Object;

            mockMember.Setup(mockObject => mockObject.GetUserId()).Returns(1);
            mock.Setup(mockObject => mockObject.OwnedStore(1)).Returns(new Store());


            Assert.IsInstanceOfType(manageStoreController.Index(), typeof(ActionResult));
            mock.Verify(mockObject => mockObject.OwnedStore(1));
        }

        /// <summary>
        /// Test ManageStore Post Method
        /// </summary>
        [TestMethod]
        public void ManageStorePostTest() 
        {
            var mock = new Mock<IManageStoreService>();
            var mockMember = new Mock<IMembership>();
            bool isSuccess = false;

            
            manageStoreController.membership = mockMember.Object;
            manageStoreController.adminService = mock.Object;
            mockMember.Setup(m => m.GetUserId()).Returns(2);
            mock.Setup(m => m.OwnedStore(2)).Returns(storeOne);
            mock.Setup(m => m.EditStore(storeOne, storeTwo, out isSuccess)).Returns(new Store());
            //set field in adminController
            

            Assert.IsInstanceOfType(manageStoreController.
                Index(storeTwo), typeof(JsonResult));
        }


        /// <summary>
        /// Helper class for ManageStoreGetTestNotNull()
        /// </summary>
        private class FakeMembershipUser : MembershipUser
        {
            public FakeMembershipUser() { }
        }
        /// <summary>
        /// Tests ManageStore Get request where MembershipUser is not null
        /// </summary>
        [TestMethod]
        public void ManageStoreGetTestNotNull()
        {
            var mock = new Mock<IMembership>();
            var mockService = new Mock<IManageStoreService>();
            MembershipUser user = new FakeMembershipUser();
            manageStoreController.membership = mock.Object;
            manageStoreController.adminService = mockService.Object;
            mock.Setup(m => m.GetUser()).Returns(user);

            mockService.Setup(m => m.OwnedStore(1)).Returns(new Store());
            mock.Setup(m => m.GetUserId()).Returns(1);


            Assert.IsInstanceOfType(manageStoreController.Index(), typeof(ViewResult));
        }

    }
}
