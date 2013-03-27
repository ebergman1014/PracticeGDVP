using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Auth;
using CardShop.Utilities;
using Moq;
using System.Web;
using CardShop.Models;

namespace CardShopTest.AuthTests
{
    [TestClass]
    public class UserAuthTest
    {
        IUserAuth auth = new UserAuth();
        Mock<IFactory> factory;
        Mock<IHttpContext> context;
        Mock<ISession> session;
        User user = new User();
        User actingAs = new User();

        [TestInitialize]
        public void Setup()
        {
            context = new Mock<IHttpContext>();
            factory = new Mock<IFactory>();
            session = new Mock<ISession>();
            context.SetupGet(c => c.Session).Returns(session.Object);
            session.SetupGet(s => s["__UserAuth"]).Returns(auth);
            Factory.Instance = factory.Object;
            user.RoleId = (int)Role.User;
            auth.User = user;
            user.IsActive = true;
            actingAs.RoleId = (int)Role.Admin;
            actingAs.IsActive = true;
        }
        [TestMethod]
        public void TestHasRoleAndActive()
        {
            Assert.IsTrue(auth.HasRole(Role.User));
        }
        [TestMethod]
        public void TestHasRoleAndInactive()
        {
            user.IsActive = false;
            Assert.IsFalse(auth.HasRole(Role.User));
        }
        [TestMethod]
        public void TestMissingRole()
        {
            Assert.IsFalse(auth.HasRole(Role.Admin));
        }
        [TestMethod]
        public void TestActingAsRole()
        {
            auth.ActingAs = actingAs;
            Assert.IsTrue(auth.HasRole(Role.Admin));
        }

        [TestMethod]
        public void TestEmptyRolesLoggedIn()
        {
            Assert.IsTrue(auth.HasRole());
        }

        [TestMethod]
        public void TestEmptyRolesLoggedOut()
        {
            auth.User = null;
            Assert.IsFalse(auth.HasRole());
        }

        [TestMethod]
        public void TestIsLoggedInAndOut()
        {
            Assert.IsTrue(auth.IsLoggedIn());
            auth.Logout();
            Assert.IsFalse(auth.IsLoggedIn());
        }

        [TestMethod]
        public void TestActingAs()
        {
            Assert.AreSame(user, auth.ActingUser);
            auth.ActingAs = actingAs;
            Assert.AreSame(actingAs, auth.ActingUser);
        }

        [TestMethod]
        public void TestGetUserAuthFromContext()
        {

            factory.Setup(f => f.Create<ContextWrapper, IHttpContext>(context)).Returns(context.Object);
            Assert.AreSame(auth, UserAuth.GetUserAuth(context.Object));
        }

        [TestMethod]
        public void TestGetUserAuthFromCurrentContext()
        {

            factory.Setup(f => f.Create<ContextWrapper, IHttpContext>(HttpContext.Current)).Returns(context.Object);
            Assert.AreSame(auth, UserAuth.Current);
        }
    }
}
