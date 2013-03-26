using System;
using System.Collections.Generic;
using CardShop.Daos;
using System.Linq;
using CardShop.Models;
using System.Data.Entity;
using CardShop.Service.Admin;
using CardShopTest.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using System.Web;
using CardShop.Auth;
using System.Data.Objects;


namespace CardShopTest.ServiceTests.Admin
{
    [TestClass]
    public class ManageUserServiceTests
    {
        private ManageUserService manageUserService = (ManageUserService)ManageUserService.GetInstance();
        private const int USER_FOUR = 4;
        private const int FIRST_USER = 0;
        private bool isSuccess;
        private List<User> listOfUsersTest = ListOfUsers.GetListOfUsers(USER_FOUR);


        private User userTest = ListOfUsers.GetListOfUsers(USER_FOUR)[FIRST_USER];
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users().Find(USER_FOUR)).Returns(userTest);
            Assert.AreEqual<User>(manageUserService.DeleteUser(USER_FOUR, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);

            mock.Setup(mockObject => mockObject.Users().Find(USER_FOUR)).Returns((User)null);
            Assert.AreEqual<User>(manageUserService.DeleteUser(USER_FOUR, out isSuccess), null);
            Assert.IsFalse(isSuccess);

            mock.Verify(mockObject => mockObject.SaveChanges());
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            manageUserService.db = mock.Object;
            // IN PROGRESS.
           // mock.Setup(mockObject => mockObject.Users().Include("aa")).Returns(listOfUsersTest);
        }
    }
}
