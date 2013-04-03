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
        private RulesetDetails testRuleDetails;
        private List<RuleSet> mockRuleSets;
        private RuleController testController;
        private const int TESTIDONE = 1;
        private const int TESTIDTWO = 2;
        private const int TESTIDTHREE = 3;

        [TestInitialize]
        public void InitializeTest()
        {
            mockRuleService = new Mock<IRuleService>();
            testRuleDetails = new RulesetDetails();
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

            testRuleDetails.rulesetWrapper = mockRuleSets[0];

            // Setup mock behavior for the GetAllFish() method in our repository
            mockRuleService.Setup(rules => rules.GetAllRulesets()).Returns(mockRuleSets);
            mockRuleService.Setup(rules => rules.Details(TESTIDONE)).Returns(testRuleDetails);

            //testRuleDetails.rulesetWrapper = mockRuleSets[1];
            //mockRuleService.Setup(rules => rules.Create(mockRuleSets[1])).Returns(testRuleDetails);
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
        public void TextEditDetailsWithTestIdOne()
        {
            testController = new RuleController(mockRuleService.Object);
            ViewResult result = testController.Edit(TESTIDONE) as ViewResult;
            RulesetDetails ruleset = (RulesetDetails)result.Model;

            Assert.IsNotNull(ruleset, "The rule's details weren't returned.");
            Assert.AreEqual(TESTIDONE, ruleset.rulesetWrapper.RuleSetId);
        }

    }
}
