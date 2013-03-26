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
using System.Data.Entity.Infrastructure;
using System.Text;



namespace CardShopTest.ServiceTests.Admin
{
    [TestClass]
    public class ManageUserServiceTests
    {
        private ManageUserService manageUserService = (ManageUserService)ManageUserService.GetInstance();
        private const int USER_FOUR = 4;
        private const int FIRST_USER = 0;
        private const string stupidPath = "you are a dumb path";
        private bool isSuccess;
        private StringBuilder _stringBuilder = new StringBuilder();
        //        private DbQuery<User> users = new DbQueryTest<User>();
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

        /// <summary>
        ///  NEEDS FIXIN
        /// </summary>
        public void GetAllUsersTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();


            manageUserService.db = mock.Object;

            mock.Setup(m => m.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(m => m.Include(It.IsAny<String>())).Returns((DbQuery<User>)mockDbSet.Object);

            manageUserService.GetAllUsers(out isSuccess);
        }

        [TestMethod]
        public void GetUserGoodTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();
            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns(userTest);

            Assert.AreSame(manageUserService.GetUser(USER_FOUR, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);

        }
        [TestMethod]
        public void GetUserBadTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();
            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns((User)null);

            Assert.AreSame(manageUserService.GetUser(USER_FOUR, out isSuccess), null);
            Assert.IsFalse(isSuccess);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();
            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(mockObject => mockObject.Add(userTest)).Returns((User)userTest);

            Assert.AreSame(manageUserService.CreateUser(userTest, out isSuccess), userTest);
            Assert.IsTrue(isSuccess);

            mock.Verify(mockObject => mockObject.SaveChanges());

        }

        [TestMethod]
        public void GetRoleViewTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<webpages_Roles>>();
            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.webpages_Roles()).Returns(mockDbSet.Object);
            Assert.IsInstanceOfType(manageUserService.GetRoleView(out isSuccess), typeof(IEnumerable));
            Assert.IsTrue(isSuccess);

            mock.Verify(mockObject => mockObject.webpages_Roles());
        }

        [TestMethod]
        public void EditUserFailTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();

            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);

            mockDbSet.Setup(mockObject => mockObject.Find(userTest.UserId)).Returns((User)null);
            Assert.IsInstanceOfType(manageUserService.EditUser(userTest, out isSuccess), typeof(User));
            Assert.IsFalse(isSuccess);

        }

        [TestMethod]
        public void EditUserPassTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();

            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);

            mockDbSet.Setup(mockObject => mockObject.Find(userTest.UserId)).Returns(userTest);
            Assert.IsInstanceOfType(manageUserService.EditUser(userTest, out isSuccess), typeof(User));
            Assert.IsTrue(isSuccess);
            mock.Verify(mockObject => mockObject.SaveChanges());
        }

        [TestMethod]
        public void ActAsUserFailTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();

            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns((User)null);

            manageUserService.ActAsUser(USER_FOUR, out isSuccess);
            Assert.IsFalse(isSuccess);
        }

        // maybe do later. Bad codes for now. Need MORE WRAPPERS!
        public void ActAsUserPassTest()
        {
            var mock = new Mock<IPracticeGDVPDao>();
            var mockDbSet = new Mock<IDbSet<User>>();
            var response = new Mock<HttpResponseBase>();
            response.Setup(x => x.Write(It.IsAny<string>())).Callback<string>(y => _stringBuilder.Append(y));
            
            var url = new Uri("http://localhost/Home/");
            
            var request = new Mock<HttpRequestBase>();
            request.Setup(x => x.Url).Returns(url);
            request.Setup(x => x.ApplicationPath).Returns("");
            
            var httpContext = new Mock<HttpContextBase>();
            httpContext.Setup(x => x.Request).Returns(request.Object);
            httpContext.Setup(x => x.Response).Returns(response.Object);


            manageUserService.db = mock.Object;

            mock.Setup(mockObject => mockObject.Users()).Returns(mockDbSet.Object);
            mockDbSet.Setup(mockObject => mockObject.Find(USER_FOUR)).Returns(userTest);

            manageUserService.ActAsUser(USER_FOUR, out isSuccess);
            Assert.IsTrue(isSuccess);
        }



    }
}
