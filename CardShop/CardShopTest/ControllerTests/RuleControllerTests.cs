using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Workflow.Activities;
using CardShop.Controllers;
using CardShop.Models;
using CardShop.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class RuleControllerTests
    {
        private RuleController Controller { get; set; }
        private Mock<CardShop.Models.RuleSet> RulesetWrapper { get; set; }
        private Mock<RuleSet> Ruleset { get; set; }
        private Mock<RulesetDetails> RulesetDetails { get; set; }
        private Mock<HttpRequestBase> Request { get; set; }
        private Mock<HttpResponseBase> Response { get; set; }

        public RuleControllerTests()
        {
            Controller = new RuleController();

            Request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            Request.SetupGet(x => x.ApplicationPath).Returns("/");
            Request.SetupGet(x => x.Url).Returns(new Uri("http://localhost/a", UriKind.Absolute));
            Request.SetupGet(x => x.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection());

            Response = new Mock<HttpResponseBase>(MockBehavior.Strict);
        }

        [TestMethod]
        public void Create_ReturnView()
        {
            string returnUrl = "Rule/Create";
            var result = Controller.Create() as ViewResult;

            Assert.IsNotNull(result);
            //Assert.AreEqual(returnUrl, result.Url);
        }

        [TestMethod]
        public void Create_Post()
        {
            //var result = Controller.Create(RulesetDetails);
        }
    }
}
