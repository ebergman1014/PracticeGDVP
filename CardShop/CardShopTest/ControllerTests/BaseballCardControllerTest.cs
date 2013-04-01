﻿using System;
using Moq;
using CardShop.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using CardShop;
using CardShop.Daos;
using CardShop.Models;

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

        [TestMethod]
        public void UploadRedirect()
        {
            string viewName = "Upload";
            ViewResult result = Controller.Upload() as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual(viewName, result.ViewName);
        }

        [TestMethod]
        public void BrowseRedirect()
        {
            string viewName = "Browse";
            ViewResult result = Controller.Browse() as ViewResult;
            Assert.IsNotNull(result);

            Assert.AreEqual(viewName, result.ViewName);
        }

        [TestMethod]
        public void DetailsRedirect()
        {
            var testCard = new BaseballCard();
            //Controller.db = (IPracticeGDVPDao) DBContext;
            //DBContext.Setup(test => test.BaseballCards().Find(3)).Returns(testCard);

            var result = Controller.Details(3) as ViewResult;
            Assert.IsNotNull(result);
            

            Assert.AreEqual<string>("thing", result.Model.ToString());
        }

    }
}
