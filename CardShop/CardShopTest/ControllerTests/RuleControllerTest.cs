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

            mockRuleService.Setup(rules => rules.GetAllRulesets()).Returns(mockRuleSets);
            
            mockRuleService.Setup(rules => rules.Details(TESTIDONE)).Returns(testRuleDetailsOne);
            mockRuleService.Setup(rules => rules.Details(TESTIDTWO)).Returns(testRuleDetailsTwo);
            mockRuleService.Setup(rules => rules.Details(TESTIDTHREE)).Returns(testRuleDetailsThree);
            
        }

        [TestMethod]
        public void TestIndexGetAllRuleSets()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Index() as ViewResult;
            List<RuleSet> ruleSets = (List<RuleSet>)result.ViewData.Model;

            Assert.IsNotNull(ruleSets, "The rules weren't returned.");
            Assert.AreEqual(TESTIDONE, ruleSets[0].RuleSetId);
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
            RedirectToRouteResult result = testController.Create(testRuleDetailsOne) as RedirectToRouteResult;
            mockRuleService.Verify();

            Assert.IsNotNull(result, "No RedirectResult thingy was returned.");
           // Assert.AreEqual("Index", result.RouteValues);
        }

        [TestMethod]
        public void TestEditDetailsGET()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Edit(TESTIDTWO) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;
            short status = 1;

            Assert.IsNotNull(ruleset, "The rule's details weren't returned.");
            Assert.AreEqual(TESTIDTWO, ruleset.rulesetWrapper.RuleSetId);
            Assert.AreEqual("Second Test", ruleset.rulesetWrapper.RuleSet1);
            Assert.AreEqual("Test RuleSet 2", ruleset.rulesetWrapper.Name);
            Assert.AreEqual(status, ruleset.rulesetWrapper.Status);
           
        }

        [TestMethod]
        public void TestDetailsGetId()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Details(TESTIDONE) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;

            Assert.IsNotNull(ruleset, "The rule's Details weren't retrieved.");
            Assert.AreEqual(mockRuleSets[0].RuleSet1,
                ruleset.rulesetWrapper.RuleSet1,
                "The details returned weren't correct.");
        }

        [TestMethod]
        public void TestDeleteRuleset()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Delete(TESTIDTHREE) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;

            Assert.IsNotNull(ruleset, "The rule selected for deletion wasn't returned.");
            Assert.AreEqual(TESTIDTHREE, ruleset.rulesetWrapper.RuleSetId);
        }

        [TestMethod]
        public void TestSomething()
        {

        }

    }
}
