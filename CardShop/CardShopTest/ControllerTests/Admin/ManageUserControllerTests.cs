using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShopTest.TestHelper;
using CardShop.Controllers;
using Moq;
using CardShop.Service;
using CardShop.Service.Admin;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Auth;
using System.Web.Security;

namespace CardShopTest.ControllerTests.Admin
{
    [TestClass]
    public class ManageUserControllerTests
    {
        ManageUserController manageUserController;
        bool isSuccess;
        private const int USER_FOUR = 4;

        [TestInitialize]
        public void Setup()
        {
            manageUserController = new ManageUserController();
        }

        [TestMethod]
        public void ManageUserIndexTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;
            Assert.IsInstanceOfType(manageUserController.Index(), typeof(ViewResult));
            mock.Verify(mockObject => mockObject.GetAllUsers(out isSuccess));

        }

        [TestMethod]
        public void ManageUserDetailsTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;
            // No object made, so will be a null user
            Assert.IsInstanceOfType(manageUserController.Details(USER_FOUR), typeof(HttpNotFoundResult));

            mock.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(ListOfUsers.GetListOfUsers(USER_FOUR)[0]);
            Assert.IsInstanceOfType(manageUserController.Details(USER_FOUR), typeof(ViewResult));
        }


        [TestMethod]
        public void ManageUserCreateTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;
            Assert.IsInstanceOfType(manageUserController.Create(), typeof(ViewResult));
            mock.Verify(mockObject => mockObject.GetRoleView(out isSuccess));
        }

        [TestMethod]
        public void ManageUserCreateUserTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;
            var mockMembership = new Mock<IMembership>();
            
            manageUserController.membership = mockMembership.Object;
            Assert.IsInstanceOfType(manageUserController.Create(ListOfUsers.GetListOfUsers(USER_FOUR)[0]), typeof(RedirectToRouteResult));
           
            
            mockMembership.Setup(mockObject => mockObject.GetUser()).Returns(new GoodUser());
            Assert.IsInstanceOfType(manageUserController.Create(ListOfUsers.GetListOfUsers(USER_FOUR)[0]), typeof(ViewResult));
        }
    }

    public class GoodUser : MembershipUser { }
}
