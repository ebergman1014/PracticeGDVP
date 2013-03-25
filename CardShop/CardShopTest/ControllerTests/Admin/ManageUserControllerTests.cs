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
        User userTest;

        [TestInitialize]
        public void Setup()
        {
            manageUserController = new ManageUserController();
            userTest = ListOfUsers.GetListOfUsers(USER_FOUR)[0];
        }

        [TestMethod]
        public void ManageUserIndexTest()
        {
            // A mock, you willl see this everywhere!
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;

            // Verifying 
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

            mock.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(userTest);
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

            Assert.IsInstanceOfType(manageUserController.Create(userTest), typeof(RedirectToRouteResult));

            manageUserController.ModelState.AddModelError("bad", new Exception());
            Assert.IsInstanceOfType(manageUserController.Create(userTest), typeof(ViewResult));
        }

        [TestMethod]
        public void ManageUserEditTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;

            mock.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns((User)null);
            Assert.IsInstanceOfType(manageUserController.Edit(USER_FOUR), typeof(HttpNotFoundResult));

            mock.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserController.Edit(USER_FOUR), typeof(ViewResult));
            mock.Verify(mockObject => mockObject.GetRoleView(out isSuccess));
        }

        [TestMethod]
        public void ManageUserEditUserTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;

            mock.Setup(mockObject => mockObject.EditUser(userTest, out isSuccess)).Returns(new User());
            Assert.IsInstanceOfType(manageUserController.Edit(userTest), typeof(RedirectToRouteResult));

            manageUserController.ModelState.AddModelError("bad", new Exception());
            Assert.IsInstanceOfType(manageUserController.Edit(userTest), typeof(ViewResult));

            mock.Verify(mockObject => mockObject.GetRoleView(out isSuccess));


        }

        [TestMethod]
        public void ManageUserDeleteTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;

            mock.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserController.Delete(USER_FOUR), typeof(ViewResult));

            mock.Setup(mockObject => mockObject.GetUser(USER_FOUR, out isSuccess)).Returns((User)null);
            Assert.IsInstanceOfType(manageUserController.Delete(USER_FOUR), typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void ManageUserDeleteConfirmedTest()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;

            Assert.IsInstanceOfType(manageUserController.DeleteConfirmed(USER_FOUR), typeof(RedirectToRouteResult));

            mock.Verify(mockObject => mockObject.DeleteUser(USER_FOUR, out isSuccess));

        }

        [TestMethod]
        public void ManageUserActAsUser()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;
            Assert.IsInstanceOfType(manageUserController.ActAsUser(USER_FOUR), typeof(RedirectToRouteResult));

            mock.Verify(mockObject => mockObject.ActAsUser(USER_FOUR, out isSuccess));
        }

        [TestMethod]
        public void ManageUserStopActingAsUser()
        {
            var mock = new Mock<IManageUserService>();
            manageUserController.manageUserService = mock.Object;
            Assert.IsInstanceOfType(manageUserController.StopActingAsUser(), typeof(RedirectToRouteResult));

            mock.Verify(mockObject => mockObject.StopActingAsUser(out isSuccess));
        }
    }
}
