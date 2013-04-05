using System;
using System.Collections.Generic;
using System.Web.Mvc;
using CardShop.Controllers;
using CardShop.Models;
using CardShop.Service;
using CardShop.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class RuleControllerTest
    {

        private Mock<IRuleService> mockRuleService;
        private RulesetDetails testRuleDetailsOne;
        private RulesetDetails testRuleDetailsTwo;
        private RulesetDetails testRuleDetailsThree;
        private List<RuleSet> mockRuleSets;
        private RuleController testController;
        private const int TESTIDONE = 1;
        private const int TESTIDTWO = 2;
        private const int TESTIDTHREE = 3;

        [TestInitialize]
        public void InitializeTest()
        {
            mockRuleService = new Mock<IRuleService>();
            testRuleDetailsOne = new RulesetDetails();
            testRuleDetailsTwo = new RulesetDetails();
            testRuleDetailsThree = new RulesetDetails();
            mockRuleSets = new List<RuleSet>();

            mockRuleSets.Add(new RuleSet()
            {
                Name = "Test RuleSet 1",
                RuleSet1 = "First Test",
                RuleSetId = 1,
                Status = 1,
            });

            mockRuleSets.Add(new RuleSet()
            {
                Name = "Test RuleSet 2",
                RuleSet1 = "Second Test",
                RuleSetId = 2,
                Status = 1,
            });

            mockRuleSets.Add(new RuleSet()
            {
                Name = "Test RuleSet 3",
                RuleSet1 = "Third Test",
                RuleSetId = 3,
                Status = 1,
            });

            testRuleDetailsOne.rulesetWrapper = mockRuleSets[0];
            testRuleDetailsTwo.rulesetWrapper = mockRuleSets[1];
            testRuleDetailsThree.rulesetWrapper = mockRuleSets[2];


        }
        [TestMethod]
        public void TestIndexGetAllRuleSets()
        {
            mockRuleService.Setup(rules => rules.GetAllRulesets())
                .Returns(mockRuleSets).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Index() as ViewResult;
            List<RuleSet> ruleSets = (List<RuleSet>)result.ViewData.Model;

            Assert.IsNotNull(ruleSets, "The rules weren't returned.");
            Assert.AreEqual(TESTIDONE, ruleSets[0].RuleSetId);
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestCreateReturnView()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Create() as ViewResult;

            Assert.IsNotNull(result, "No View was returned.");
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void TestCreateRule()
        {
            mockRuleService.Setup(rules => rules.Create(mockRuleSets[0])).
                Returns(mockRuleSets[1]).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            RedirectToRouteResult result = testController
                .Create(testRuleDetailsOne) as RedirectToRouteResult;

            Assert.IsNotNull(result, "No RedirectResult thingy was returned.");
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestEditDetailsGET()
        {
            mockRuleService.Setup(rules => rules.Details(TESTIDTWO))
                .Returns(testRuleDetailsTwo).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Edit(TESTIDTWO) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;
            short status = 1;

            Assert.IsNotNull(ruleset, "The rule's details weren't returned.");
            Assert.AreEqual(TESTIDTWO, ruleset.rulesetWrapper.RuleSetId);
            Assert.AreEqual("Second Test", ruleset.rulesetWrapper.RuleSet1);
            Assert.AreEqual("Test RuleSet 2", ruleset.rulesetWrapper.Name);
            Assert.AreEqual(status, ruleset.rulesetWrapper.Status);
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestEditDetails()
        {
            mockRuleService.Setup(rules => rules.Edit(mockRuleSets[0]))
                .Returns(mockRuleSets[0]).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            RedirectToRouteResult result = testController
                .Edit(testRuleDetailsOne) as RedirectToRouteResult;

            Assert.IsNotNull(result);
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestEditDetailsHttpReturn()
        {
            RulesetDetails testNullRulesetDetails = null;
            mockRuleService.Setup(rules => rules.Details(TESTIDONE))
                .Returns(testNullRulesetDetails).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            var result = testController.Edit(TESTIDONE) as HttpNotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
            mockRuleService.Verify();
        }


        [TestMethod]
        public void TestDetailsGetId()
        {
            mockRuleService.Setup(rules => rules.Details(TESTIDONE))
                .Returns(testRuleDetailsOne).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Details(TESTIDONE) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;

            Assert.IsNotNull(ruleset, "The rule's Details weren't retrieved.");
            Assert.AreEqual(mockRuleSets[0].RuleSet1,
                ruleset.rulesetWrapper.RuleSet1,
                "The details returned weren't correct.");
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestDetailsGETHttpReturn()
        {
            RulesetDetails testNullRulesetDetails = null;
            mockRuleService.Setup(rules => rules.Details(TESTIDONE))
                .Returns(testNullRulesetDetails).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            var result = testController.Details(TESTIDONE) as HttpNotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestDeleteRuleset()
        {
            mockRuleService.Setup(rules => rules.Details(TESTIDTHREE))
                .Returns(testRuleDetailsThree).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Delete(TESTIDTHREE) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;

            Assert.IsNotNull(ruleset, "The rule selected for deletion wasn't returned.");
            Assert.AreEqual(TESTIDTHREE, ruleset.rulesetWrapper.RuleSetId);
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestDeleteRulesetHttpReturn()
        {
            RulesetDetails testNullRulesetDetails = null;
            mockRuleService.Setup(rules => rules.Details(TESTIDTWO))
                .Returns(testNullRulesetDetails).Verifiable();

            testController = new RuleController(mockRuleService.Object);
            var result = testController.Delete(TESTIDTWO) as HttpNotFoundResult;

            Assert.AreEqual(404, result.StatusCode);
            mockRuleService.Verify();
        }

        [TestMethod]
        public void TestDeleteConfirmed()
        {
            mockRuleService.Setup(ruleService => ruleService.Delete(TESTIDTHREE))
                .Verifiable();

            testController = new RuleController(mockRuleService.Object);
            RedirectToRouteResult result = testController
                .DeleteConfirmed(TESTIDTHREE) as RedirectToRouteResult;

            Assert.IsNotNull(result, "No RedirectResult thingy was returned.");
            mockRuleService.Verify();
        }

        [TestMethod]
        public void UploadViewReturn()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Upload() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Upload", result.ViewName);
        }

    }
}
