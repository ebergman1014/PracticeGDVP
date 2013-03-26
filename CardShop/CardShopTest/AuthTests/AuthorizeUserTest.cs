using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Auth;
using System.Web;
using CardShop.Models;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Reflection;
using Moq;

namespace CardShopTest.AuthTests
{
    [TestClass]
    public class AuthorizeUserTest
    {

        private AuthorizeUserAttribute authCore;
        private Mock<IWrapperFactory> factory;
        private Mock<IHttpContext> context;
        private Mock<ISession> session;
        private Mock<IUserAuth> auth;

        [TestInitialize]
        public void Setup()
        {
            this.authCore = new AuthorizeUserAttribute();
            factory = new Mock<IWrapperFactory>();
            context = new Mock<IHttpContext>();
            session = new Mock<ISession>();
            auth = new Mock<IUserAuth>();
            WrapperFactory.Factory = factory.Object;
            AuthorizeUserAttribute authCore = new AuthorizeUserAttribute();
            factory.Setup(f => f.Wrap<ContextWrapper, IHttpContext>(It.IsAny<HttpContextBase>())).Returns(context.Object);
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
                new Object[]{null}));
        }
        [TestMethod]
        public void TestAuthCoreNegative()
        {
            auth.Setup(a => a.HasRole(It.IsAny<Role[]>())).Returns(false);
            Assert.IsFalse((bool)authCore.GetType().InvokeMember("AuthorizeCore",
                BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                authCore,
                new Object[]{null}));
        }
    }
}
