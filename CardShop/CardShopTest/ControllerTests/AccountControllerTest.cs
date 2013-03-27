using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using CardShop.Controllers;
using CardShop.Models;
using CardShop.Filters;
using Moq;
using System.Web;
using System.Security.Principal;
using System.Web.Routing;
using CardShop;
using Microsoft.Web.WebPages.OAuth;
using System.Web.Security;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController Controller { get; set; }
        private RouteCollection Routes { get; set; }
        private Mock<IWebSecurity> WebSecurity { get; set; }
        private Mock<IOAuthWebSecurity> OAuthWebSecurity { get; set; }
        private Mock<HttpResponseBase> Response { get; set; }
        private Mock<HttpRequestBase> Request { get; set; }
        private Mock<HttpContextBase> Context { get; set; }
        private Mock<ControllerContext> ControllerContext { get; set; }
        private Mock<IPrincipal> User { get; set; }
        private Mock<IIdentity> Identity { get; set; }

        public AccountControllerTests()
        {
            WebSecurity = new Mock<IWebSecurity>(MockBehavior.Strict);
            OAuthWebSecurity = new Mock<IOAuthWebSecurity>(MockBehavior.Strict);

            Identity = new Mock<IIdentity>(MockBehavior.Strict);
            User = new Mock<IPrincipal>(MockBehavior.Strict);
            User.SetupGet(u => u.Identity).Returns(Identity.Object);
            WebSecurity.SetupGet(w => w.CurrentUser).Returns(User.Object);

            Routes = new RouteCollection();
            RouteConfig.RegisterRoutes(Routes);

            Request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            Request.SetupGet(x => x.ApplicationPath).Returns("/");
            Request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/a", UriKind.Absolute));
            Request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            Response = new Mock<HttpResponseBase>(MockBehavior.Strict);

            Context = new Mock<HttpContextBase>(MockBehavior.Strict);
            Context.SetupGet(x => x.Request).Returns(Request.Object);
            Context.SetupGet(x => x.Response).Returns(Response.Object);

            Controller = new AccountController(WebSecurity.Object, OAuthWebSecurity.Object);
            Controller.ControllerContext = new ControllerContext(Context.Object, new RouteData(), Controller);
            Controller.Url = new UrlHelper(new RequestContext(Context.Object, new RouteData()), Routes);
        }

        [TestMethod]
        public void Login_ReturnUrlSet()
        {
            string returnUrl = "/Home/Index";
            var result = Controller.Login(returnUrl) as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual(returnUrl, Controller.ViewBag.ReturnUrl);
        }

        [TestMethod]
        public void Login_UserCanLogin()
        {
            string returnUrl = "/Home/Index";
            string userName = "user";
            string password = "password";

            WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(true);
            var model = new LoginModel
            {
                UserName = userName,
                Password = password
            };

            var result = Controller.Login(model, returnUrl) as RedirectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(returnUrl, result.Url);
        }

        [TestMethod]
        public void Login_InvalidCredentialsRedisplaysLoginScreen()
        {
            string returnUrl = "/Home/Index";
            string userName = "user";
            string password = "password";

            WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(false);
            var model = new LoginModel
            {
                UserName = userName,
                Password = password
            };

            var result = Controller.Login(model, returnUrl) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LogOff_UserCanLogOut()
        {
            WebSecurity.Setup(s => s.Logout()).Verifiable();

            var result = Controller.LogOff() as RedirectToRouteResult;
            Assert.IsNotNull(result);

            WebSecurity.Verify(s => s.Logout(), Times.Exactly(1));
        }

        [TestMethod]
        public void Register()
        {
            var result = Controller.Register() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Register_UserCanRegister()
        {
            string userName = "user";
            string password = "passweord";

            WebSecurity.Setup(s => s.CreateUserAndAccount(userName, password, null, false)).Returns(userName);
            WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(true);

            var model = new RegisterModel
            {
                UserName = userName,
                Password = password,
                ConfirmPassword = password
            };

            var result = Controller.Register(model) as RedirectToRouteResult;
            Assert.IsNotNull(result);

            WebSecurity.Verify(s => s.CreateUserAndAccount(userName, password, null, false), Times.Exactly(1));
            WebSecurity.Verify(s => s.Login(userName, password, false), Times.Exactly(1));
        }

        [TestMethod]
        public void Register_ErrorWhileRegisteringCausesFormToBeRedisplayed()
        {
            string userName = "user";
            string password = "passweord";

            WebSecurity.Setup(s => s.CreateUserAndAccount(userName, password, null, false)).Returns(userName);
            WebSecurity.Setup(s => s.Login(userName, password, false)).Throws(
            new MembershipCreateUserException(MembershipCreateStatus.InvalidEmail));

            var model = new RegisterModel
            {
                UserName = userName,
                Password = password,
                ConfirmPassword = password
            };

            var result = Controller.Register(model) as ViewResult;
            Assert.IsNotNull(result);

            Assert.IsFalse(Controller.ModelState.IsValid);
        }

        [TestMethod]
        public void Disassociate_UserCanRemoveOAuthProvider()
        {
            string provider = "twitter";
            string providerId = "Id";
            string userName = "user";
            int userId = 100;
            var accounts = new List<OAuthAccount>
            {
                new OAuthAccount(provider, providerId)
            };

            OAuthWebSecurity.Setup(o => o.GetUserName(provider, providerId)).Returns(userName);
            Identity.SetupGet(i => i.Name).Returns(userName);
            WebSecurity.Setup(s => s.GetUserId(userName)).Returns(userId);
            OAuthWebSecurity.Setup(o => o.HasLocalAccount(userId)).Returns(true);
            OAuthWebSecurity.Setup(o => o.GetAccountsFromUserName(userName)).Returns(accounts);
            OAuthWebSecurity.Setup(o => o.DeleteAccount(provider, providerId)).Returns(true);

            var result = Controller.Disassociate(provider, providerId) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.RouteValues["Message"]);

            OAuthWebSecurity.Verify(o => o.DeleteAccount(provider, providerId), Times.Exactly(1));
        }

        [TestMethod]
        public void Disassociate_RedirectsIfDoesNotMatchLoggedInUser()
        {
            string provider = "twitter";
            string providerId = "Id";
            string userName = "user";
            int userId = 100;
            var accounts = new List<OAuthAccount>
            {
                new OAuthAccount(provider, providerId)
            };

            OAuthWebSecurity.Setup(o => o.GetUserName(provider, providerId)).Returns(userName);
            Identity.SetupGet(i => i.Name).Returns("differentUser");
            OAuthWebSecurity.Setup(o => o.DeleteAccount(provider, providerId)).Returns(false);

            var result = Controller.Disassociate(provider, providerId) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsNull(result.RouteValues["Message"]); // Message should never be set

            // DeleteAccount should not have been called
            OAuthWebSecurity.Verify(o => o.DeleteAccount(provider, providerId), Times.Never());
        }

    }
}
