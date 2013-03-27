using System;
using System.Collections.Generic;
using System.Collections;
using CardShop.Daos;
using System.Linq;
using System.Linq.Expressions;
using CardShop.Models;

using CardShop.Service.Admin;
using CardShopTest.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using CardShop.Auth;
using System.Data.Objects;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;



namespace CardShopTest.ServiceTests.Admin
{
    [TestClass]
    public class ManageUserServiceTests
    {
        // manage
        private ManageUserService manageUserService = (ManageUserService)ManageUserService.GetInstance();
        private bool isSuccess;
        private StringBuilder _stringBuilder = new StringBuilder();


        //constants needed for tests
        private const int USER_FOUR = 4;
        private const int FIRST_USER = 0;
        private const string stupidPath = "you are a dumb path";

        // MOCKS
        Mock<IPracticeGDVPDao> mockIPracticeGDVPDao = new Mock<IPracticeGDVPDao>();
        Mock<IDbSet<User>> mockDbSetUser = new Mock<IDbSet<User>>();
        Mock<IDbSet<webpages_Roles>> mockDbSetWebpageRoles = new Mock<IDbSet<webpages_Roles>>();

        //        private DbQuery<User> users = new DbQueryTest<User>();
        private List<User> listOfUsersTest = ListOfUsers.GetListOfUsers(USER_FOUR);


        private User userTest = ListOfUsers.GetListOfUsers(USER_FOUR)[FIRST_USER];

        [TestInitialize]
        public void Setup()
        {
            // set our field object to the mock object
            manageUserService.db = mockIPracticeGDVPDao.Object;
        }


        [TestMethod]
        public void DeleteUserPassTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users().Find(USER_FOUR)).Returns(userTest);
            Assert.AreEqual<User>(manageUserService.DeleteUser(USER_FOUR, out isSuccess), userTest);
            mockIPracticeGDVPDao.Verify(mockObject => mockObject.SaveChanges());
            Assert.IsTrue(isSuccess);
        }

        [TestMethod]
        public void DeleteUserFailTest()
        {
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users().Find(USER_FOUR)).Returns((User)null);
            Assert.AreEqual<User>(manageUserService.DeleteUser(USER_FOUR, out isSuccess), null);
            Assert.IsFalse(isSuccess);
        }



        /// <summary>
        ///  NEEDS FIXIN
        /// </summary>
        public void GetAllUsersTest()
        {


            mockIPracticeGDVPDao.Setup(m => m.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(m => m.Include(It.IsAny<String>())).Returns((DbQuery<User>)mockDbSetUser.Object);

            manageUserService.GetAllUsers(out isSuccess);
        }

        [TestMethod]
        public void GetUserGoodTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns(userTest);

            Assert.AreSame(manageUserService.GetUser(USER_FOUR, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);

        }
        [TestMethod]
        public void GetUserBadTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns((User)null);

            Assert.AreSame(manageUserService.GetUser(USER_FOUR, out isSuccess), null);
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void CreateUserTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Add(userTest)).Returns((User)userTest);

            Assert.AreSame(manageUserService.CreateUser(userTest, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);

            mockIPracticeGDVPDao.Verify(mockObject => mockObject.SaveChanges());

        }

        [TestMethod]
        public void GetRoleViewTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.webpages_Roles()).Returns(mockDbSetWebpageRoles.Object);
            Assert.IsInstanceOfType(manageUserService.GetRoleView(out isSuccess), typeof(IEnumerable));
            Assert.IsTrue(isSuccess);

            mockIPracticeGDVPDao.Verify(mockObject => mockObject.webpages_Roles());
        }

        [TestMethod]
        public void EditUserFailTest()
        {

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);

            mockDbSetUser.Setup(mockObject => mockObject.Find(userTest.UserId)).Returns((User)null);
            Assert.IsInstanceOfType(manageUserService.EditUser(userTest, out isSuccess), typeof(User));
            Assert.IsFalse(isSuccess);

        }

        [TestMethod]
        public void EditUserPassTest()
        {
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);

            mockDbSetUser.Setup(mockObject => mockObject.Find(userTest.UserId)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserService.EditUser(userTest, out isSuccess), typeof(User));
            Assert.IsTrue(isSuccess);
            mockIPracticeGDVPDao.Verify(mockObject => mockObject.SaveChanges());
        }

        [TestMethod]
        public void ActAsUserFailTest()
        {
            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns((User)null);

            manageUserService.ActAsUser(USER_FOUR, out isSuccess);
            Assert.IsFalse(isSuccess);
        }

        // maybe do later. Bad codes for now. Need MORE WRAPPERS!
        public void ActAsUserPassTest()
        {
            // ignore for nows.
            var response = new Mock<HttpResponseBase>();
            response.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(y => _stringBuilder.Append(y));

            var url = new Uri("http://localhost/Home/");

            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Url).Returns(url);
            request.Setup(x => x.ApplicationPath).Returns("");

            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.Request).Returns(request.Object);
            httpContext.Setup(x => x.Response).Returns(response.Object);


            manageUserService.db = mockIPracticeGDVPDao.Object;

            mockIPracticeGDVPDao.Setup(mockObject => mockObject.Users()).Returns(mockDbSetUser.Object);
            mockDbSetUser.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns(userTest);

            manageUserService.ActAsUser(USER_FOUR, out isSuccess);
            Assert.IsTrue(isSuccess);
        }



    }
}
