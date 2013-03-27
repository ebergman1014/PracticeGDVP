using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Auth;
using System.Web;
using CardShop.Models;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Reflection;
using Moq;
using CardShop.Utilities;
using System.IO;

namespace CardShopTest.AuthTests
{
    [TestClass]
    public class AuthorizeUserTest
    {

        private AuthorizeUserAttribute authCore;
        private Mock<IFactory> factory;
        private Mock<IHttpContext> context;
        private Mock<ISession> session;
        private Mock<IUserAuth> auth;

        [TestInitialize]
        public void Setup()
        {
            this.authCore = new AuthorizeUserAttribute();
            factory = new Mock<IFactory>();
            context = new Mock<IHttpContext>();
            session = new Mock<ISession>();
            auth = new Mock<IUserAuth>();
            Factory.Instance = factory.Object;
            AuthorizeUserAttribute authCore = new AuthorizeUserAttribute();
            factory.Setup(f => f.Create<ContextWrapper, IHttpContext>(It.IsAny<HttpContextBase>())).Returns(context.Object);
            context.SetupGet(c => c.Session).Returns(session.Object);
            session.SetupGet(s => s["__UserAuth"]).Returns(auth.Object);
        }
 
        [TestMethod]
        public void TestAuthCorePositive()
        {
            auth.Setup(a => a.HasRole(It.IsAny<Role[]>())).Returns(true);
            Assert.IsTrue((bool)authCore.GetType().InvokeMember("AuthorizeCore",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                authCore,
                new object[]{null}));
        }
        [TestMethod]
        public void TestAuthCoreNegative()
        {
            auth.Setup(a => a.HasRole(It.IsAny<Role[]>())).Returns(false);
            Assert.IsFalse((bool)authCore.GetType().InvokeMember("AuthorizeCore",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                authCore,
                new object[]{null}));
        }
        [TestMethod]
        public void TestHandleUnauthorizedLoggedIn()
        {
            auth.Setup(a => a.IsLoggedIn()).Returns(true);
            AuthorizationContext context = CardShopTest.TestHelper.TestUtils.CreateAuthorizationContext();
            authCore.GetType().InvokeMember("HandleUnauthorizedRequest",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                authCore,
                new object[] { context });
            Assert.AreEqual(403, context.HttpContext.Response.StatusCode);
            Assert.IsNotNull(context.Result);
        }

        [TestMethod]
        public void TestHandleUnauthorizedNotLoggedIn()
        {
            auth.Setup(a => a.IsLoggedIn()).Returns(false);
            AuthorizationContext context = CardShopTest.TestHelper.TestUtils.CreateAuthorizationContext();
            authCore.GetType().InvokeMember("HandleUnauthorizedRequest",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                authCore,
                new object[] { context });
            Assert.IsNotNull(context.Result);
        }
    }
}
