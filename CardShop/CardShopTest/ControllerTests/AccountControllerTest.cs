using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using CardShop.Controllers;
using CardShop.Models;
using CardShop.Auth;
using CardShop.Filters;
using Moq;
using System.Web;
using System.Security.Principal;
using System.Web.Routing;
using CardShop;
using Microsoft.Web.WebPages.OAuth;
using System.Web.Security;
using Microsoft.CSharp;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private AccountController Controller { get; set; }
        private RouteCollection Routes { get; set; }
        private Mock<IWebSecurity> WebSecurity { get; set; }
        private Mock<HttpResponseBase> Response { get; set; }
        private Mock<HttpRequestBase> Request { get; set; }
        private Mock<HttpContextBase> Context { get; set; }
        private Mock<ControllerContext> ControllerContext { get; set; }
        private Mock<IPrincipal> User { get; set; }
        private Mock<IIdentity> Identity { get; set; }

        public AccountControllerTests()
        {
            WebSecurity = new Mock<IWebSecurity>(MockBehavior.Default);

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

            Controller = new AccountController(WebSecurity.Object);
            Controller.ControllerContext = new ControllerContext(Context.Object, new RouteData(), Controller);
            Controller.Url = new UrlHelper(new RequestContext(Context.Object, new RouteData()), Routes);
        }

        [TestMethod]
        public void Login_ReturnUrlSet()
        {
            string returnUrl = "/Home/Index";
            var result = Controller.Login(returnUrl) as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual(returnUrl, result.ViewData["ReturnUrl"]);
        }

        [TestMethod]
        public void Login_UserCanLogin()
        {
            string returnUrl = "/Home/Index";
            string userName = "bbob";
            string password = "321";

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
        public void Login_UserCanLoginInvalidState()
        {
            string returnUrl = "/Home/Index";
            string userName = "bbob";
            string password = "321";
            string error = "The user name or password provided is incorrect.";

            Controller.ModelState.AddModelError("error", error);

            WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(false);
            var model = new LoginModel
            {
                UserName = userName,
                Password = password
            };

            var result = Controller.Login(model, returnUrl) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.ViewData.ModelState["error"].Errors[0].ErrorMessage, error);
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
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
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
            string firstName = "Builder";
            string lastName = "Bob";
            string roleId = "1";
            string email = null;

            WebSecurity.Setup(s => s.CreateUserAndAccount(userName, password, new
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                RoleId = roleId
            }
            , false)).Returns(userName);
            WebSecurity.Setup(s => s.Login(userName, password, false)).Returns(true);

            var model = new RegisterModel
            {
                UserName = userName,
                Password = password,
                ConfirmPassword = password,
                FirstName = firstName,
                LastName = lastName,
                Email = null,
                RoleId = roleId
            };

            var result = Controller.Register(model) as RedirectToRouteResult;
            Assert.IsNotNull(result);

            WebSecurity.Verify();
        }

        [TestMethod]
        public void Register_ErrorWhileRegisteringCausesFormToBeRedisplayed()
        {
            string userName = "user";
            string password = "passweord";
            string firstName = "Builder";
            string lastName = "Bob";
            string roleId = "1";

            WebSecurity.Setup(s => s.CreateUserAndAccount(userName, password, null, false)).Returns(userName);
            WebSecurity.Setup(s => s.Login(userName, password, false)).Throws(
            new MembershipCreateUserException(MembershipCreateStatus.InvalidEmail));

            var model = new RegisterModel
            {
                UserName = userName,
                Password = password,
                ConfirmPassword = password,
                FirstName = firstName,
                LastName = lastName,
                RoleId = roleId
            };

            var result = Controller.Register(model) as ViewResult;
            Assert.IsNotNull(result);

            Assert.IsFalse(Controller.ModelState.IsValid);
        }
    }
}
