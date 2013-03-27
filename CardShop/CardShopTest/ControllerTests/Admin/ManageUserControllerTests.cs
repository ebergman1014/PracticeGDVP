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
        User userTest = ListOfUsers.GetListOfUsers(USER_FOUR)[0];

        Mock<IManageUserService> mockManageUserService = new Mock<IManageUserService>();

        [TestInitialize]
        public void Setup()
        {
            manageUserController = new ManageUserController();
            manageUserController.manageUserService = mockManageUserService.Object;
        }

        [TestMethod]
        public void ManageUserIndexTest()
        {

            // Verifying 
            Assert.IsInstanceOfType(manageUserController.Index(), typeof(ViewResult));
            mockManageUserService.Verify(mockObject => mockObject.GetAllUsers(out isSuccess));

        }

        [TestMethod]
        public void ManageUserDetailsFailTest()
        {
            // No object made, so will be a null user
            Assert.IsInstanceOfType(manageUserController.Details(USER_FOUR), typeof(HttpNotFoundResult));

        }

        [TestMethod]
        public void ManageUserDetailsPassTest()
        {
            mockManageUserService.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserController.Details(USER_FOUR), typeof(ViewResult));
        }


        [TestMethod]
        public void ManageUserCreateTest()
        {
            Assert.IsInstanceOfType(manageUserController.Create(), typeof(ViewResult));
            mockManageUserService.Verify(mockObject => mockObject.GetRoleView(out isSuccess));
        }

        [TestMethod]
        public void ManageUserCreateUserPassTest()
        {
            Assert.IsInstanceOfType(manageUserController.Create(userTest), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ManageUserCreateUserFailTest()
        {

            manageUserController.ModelState.AddModelError("bad", new Exception());
            Assert.IsInstanceOfType(manageUserController.Create(userTest), typeof(ViewResult));

        }

        [TestMethod]
        public void ManageUserEditFailTest()
        {

            mockManageUserService.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns((User)null);
            Assert.IsInstanceOfType(manageUserController.Edit(USER_FOUR), typeof(HttpNotFoundResult));
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void ManageUserEditPassTest()
        {

            mockManageUserService.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserController.Edit(USER_FOUR), typeof(ViewResult));
            mockManageUserService.Verify(mockObject => mockObject.GetRoleView(out isSuccess));
        }

        [TestMethod]
        public void ManageUserEditUserPassTest()
        {

            mockManageUserService.Setup(mockObject => mockObject.EditUser(userTest, out isSuccess)).Returns(new User());
            Assert.IsInstanceOfType(manageUserController.Edit(userTest), typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ManageUserEditUserFailTest()
        {
            manageUserController.ModelState.AddModelError("bad", new Exception());
            Assert.IsInstanceOfType(manageUserController.Edit(userTest), typeof(ViewResult));
            Assert.IsFalse(isSuccess);
            mockManageUserService.Verify(mockObject => mockObject.GetRoleView(out isSuccess));

        }

        [TestMethod]
        public void ManageUserDeletePassTest()
        {

            mockManageUserService.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserController.Delete(USER_FOUR), typeof(ViewResult));

        }

        [TestMethod]
        public void ManageUserDeleteFailTest()
        {
            mockManageUserService.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns((User)null);
            Assert.IsInstanceOfType(manageUserController.Delete(USER_FOUR), typeof(HttpNotFoundResult));
        }
        [TestMethod]
        public void ManageUserDeleteConfirmedTest()
        {

            Assert.IsInstanceOfType(manageUserController.DeleteConfirmed(USER_FOUR), typeof(RedirectToRouteResult));

            mockManageUserService.Verify(mockObject => mockObject.DeleteUser(USER_FOUR, out isSuccess));

        }

        [TestMethod]
        public void ManageUserActAsUser()
        {
            Assert.IsInstanceOfType(manageUserController.ActAsUser(USER_FOUR), typeof(RedirectToRouteResult));

            mockManageUserService.Verify(mockObject => mockObject.ActAsUser(USER_FOUR, out isSuccess));
        }

        [TestMethod]
        public void ManageUserStopActingAsUser()
        {
            Assert.IsInstanceOfType(manageUserController.StopActingAsUser(), typeof(RedirectToRouteResult));

            mockManageUserService.Verify(mockObject => mockObject.StopActingAsUser(out isSuccess));
        }
    }
}
