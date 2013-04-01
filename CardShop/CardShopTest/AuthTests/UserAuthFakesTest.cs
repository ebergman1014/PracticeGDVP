using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardShop.Auth;
using CardShop.Utilities;
using Moq;
using System.Web;
using CardShop.Models;
using System.Security.Principal;
using Microsoft.QualityTools.Testing.Fakes;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using CardShopTest.TestHelper;
using System.Web.SessionState;

namespace CardShopTest.AuthTests
{
    [TestClass]
    public class UserAuthFakesTest
    {
        IDisposable shimsContext;
        [TestInitialize]
        public void Setup()
        {
            shimsContext = ShimsContext.Create();
        }

        [TestCleanup]
        public void Cleanup()
        {
            shimsContext.Dispose();
        }

        [TestMethod]
        public void TestShimUserAuthCurrent()
        {
            User user = new User();
            user.UserId = 3;
            CardShop.Auth.Fakes.ShimUserAuth.CurrentGet = () => {
                var auth = new CardShop.Auth.Fakes.StubIUserAuth();
                auth.UserGet = () =>
                {
                    return user;
                };
                return auth;
            };
            Assert.AreSame(user, UserAuth.Current.User);
        }

        [TestMethod]
        public void TestSession()
        {
            Dictionary<object, object> dict = new Dictionary<object, object>();
            var context = new System.Web.Fakes.ShimHttpContext();
            var session = new System.Web.SessionState.Fakes.ShimHttpSessionState();
            context.SessionGet = () =>
            {
                return session;
            };
            session.ItemSetStringObject = (String str, object o) =>
            {
                dict.Add(str, o);
            };
            session.ItemGetString = (String str) =>
            {
                object o;
                dict.TryGetValue(str, out o);
                return o;
            };
            System.Web.Fakes.ShimHttpContext.CurrentGet = () =>
            {
                return context;
            };
            string key = "test";
            object value = "blah";
            HttpContext.Current.Session[key] = value;
            Assert.AreEqual(value, HttpContext.Current.Session[key]);
            object obj;
            dict.TryGetValue(key, out obj);
            Assert.AreEqual(value, obj);
        }
    }
}
