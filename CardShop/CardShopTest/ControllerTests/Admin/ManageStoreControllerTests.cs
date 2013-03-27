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
using CardShop.Daos;
using CardShop.Utilities;
using System.Web;


namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class ManageStoreControllerTests
    {
        ManageStoreController manageStoreController;
        Mock<IPracticeGDVPDao> dao;
        Mock<IFactory> factory;
        Mock<IUserAuth> auth;
        Mock<IHttpContext> context;
        Mock<ISession> session;
        List<Store> stores;
        Store storeOne;
        Store storeTwo;

        [TestInitialize]
        public void Setup() {
            dao = new Mock<IPracticeGDVPDao>();
            factory = new Mock<IFactory>();
            auth = new Mock<IUserAuth>();
            context = new Mock<IHttpContext>();
            session = new Mock<ISession>();
            factory.Setup(f => f.Create<PracticeGDVPDao, IPracticeGDVPDao>()).Returns(dao.Object);
            factory.Setup(f => f.Create<ContextWrapper,IHttpContext>(HttpContext.Current)).Returns(context.Object);
            context.SetupGet(c => c.Session).Returns(session.Object);
            session.SetupGet(s => s["__UserAuth"]).Returns(auth.Object);
            Factory.Instance = factory.Object;
            manageStoreController = new ManageStoreController();
            stores = StoreTest.CreateStores(2);
            storeOne = stores[0];
            storeTwo = stores[1];
        }

        [TestMethod]
        public void ManageStoreIndexTest()
        {
            var mock = new Mock<IManageStoreService>();
            manageStoreController.adminService = mock.Object;
            User user = new User() { UserId = 1 };
            auth.SetupGet(a => a.ActingUser).Returns(user);
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
            bool isSuccess = false;

            manageStoreController.adminService = mock.Object;

            User user = new User() { UserId = 2 };
            auth.SetupGet(a => a.ActingUser).Returns(user);
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
            var mockService = new Mock<IManageStoreService>();
            User user = new User() { UserId = 1 };
            auth.SetupGet(a => a.ActingUser).Returns(user);
            manageStoreController.adminService = mockService.Object;

            mockService.Setup(m => m.OwnedStore(1)).Returns(new Store());


            Assert.IsInstanceOfType(manageStoreController.Index(), typeof(ViewResult));
        }

    }
}
