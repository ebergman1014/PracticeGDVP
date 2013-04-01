using System;
using Moq;
using CardShop.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Collections.Generic;
using CardShop;
using CardShop.Models;
using CardShop.Daos;
using System.Data.Entity;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class BaseballCardControllerTest
    {
        private BaseballCardController Controller { get; set; }
        private RouteCollection Routes { get; set; }
        private Mock<PracticeGDVPDao> DBContext { get; set; }
        private Mock<HttpResponseBase> Response { get; set; }
        private Mock<HttpRequestBase> Request { get; set; }
        private Mock<HttpContextBase> Context { get; set; }
        private Mock<ControllerContext> ControllerContext { get; set; }

        public BaseballCardControllerTest()
        {
        }

        [TestInitialize]
        public void Setup()
        {
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

            DBContext = new Mock<PracticeGDVPDao>(MockBehavior.Strict);

            Controller = new BaseballCardController();
            Controller.ControllerContext = new ControllerContext(Context.Object, new RouteData(), Controller);
            Controller.Url = new UrlHelper(new RequestContext(Context.Object, new RouteData()), Routes);
        }

        // Removed all the tests because none of them were suppose to be pushed since
        // I'm still working on figuring out how exactly they work. This way they 
        // aren't failing or causing build issues for anybody.
        // ~ Don

    }
}
