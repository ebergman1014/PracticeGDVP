using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Workflow.Activities.Rules;
using CardShop.Daos;
using CardShop.Models;
using CardShop.Service;
using CardShop.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardShopTest.ServiceTests
{
    [TestClass]
    public class RuleServiceTest
    {
        private RuleService ruleService;
        private CardShop.Models.RuleSet rulesetWrapper;
        private List<CardShop.Models.RuleSet> rulesets;
        private System.Workflow.Activities.Rules.RuleSet ruleset;
        private RuleObject ruleObject;
        private List<RuleObject> ruleObjects;
        private RulesetDetails rulesetDetails;

        private Mock<IRuleService> mockRuleService;
        private Mock<IPracticeGDVPDao> mockContext;
        private Mock<IDbSet<CardShop.Models.RuleSet>> mockDbSet;

        private int RULESET_THREE = 3;

        [TestInitialize]
        public void SetUp()
        {
            ruleService = new RuleService();
            rulesetWrapper = new CardShop.Models.RuleSet();
            rulesets = new List<CardShop.Models.RuleSet>();
            ruleset = new System.Workflow.Activities.Rules.RuleSet();
            ruleObject = new RuleObject();
            ruleObjects = new List<RuleObject>();
            rulesetDetails = new RulesetDetails();

            mockRuleService = new Mock<IRuleService>();
            mockContext = new Mock<IPracticeGDVPDao>();
            mockDbSet = new Mock<IDbSet<CardShop.Models.RuleSet>>();

            ruleService.dbContext = mockContext.Object;
        }

        // in progress wooooooo

        //[TestMethod]
        //public void GetAllRulesetsTest()
        //{
        //    //mockContext.Setup(mock => mock.RuleSets()).Returns(mockDbSet);
        //    //mockContext.Setup(mock => mock.RuleSets().ToList()).Returns(rulesets);
        //    //mockDbSet.Setup(mock => mock.ToList()).Returns(rulesets);
        //    //Assert.AreEqual(ruleService.GetAllRulesets(), rulesets);
        //    Assert.IsNotNull(ruleService.GetAllRulesets());
        //    Assert.AreEqual(ruleService.GetAllRulesets().Count, rulesets.Count);
        //    mockContext.Verify();
        //}

        //[TestMethod]
        //public void CreateRulesetTest()
        //{
        //    List<RuleObject> rulesObject = new List<RuleObject>();
        //    rulesObject.Add(ruleObject);

        //    mockRuleService.Setup(mock => mock.DeserializeJSONRulesObject("")).Returns(rulesObject);
        //    mockRuleService.Setup(mock => mock.CompileRuleset(ruleset, rulesObject)).Returns("");

        //    ruleService.Create(rulesetWrapper);
        //}

        [TestMethod]
        public void DeleteRulesetPassTest()
        {
            mockContext.Setup(mock => mock.RuleSets().Find(RULESET_THREE)).Returns(rulesetWrapper);
            ruleService.Delete(RULESET_THREE);
            mockContext.Verify(mock => mock.RuleSets().Remove(rulesetWrapper));
            mockContext.Verify(mock => mock.SaveChanges());
        }

        [TestMethod]
        public void DeleteRulesetFailTest()
        {
            mockContext.Setup(mock => mock.RuleSets().Find(RULESET_THREE)).Returns((CardShop.Models.RuleSet)null);
            ruleService.Delete(RULESET_THREE);
            // bleh
        }

        [TestMethod]
        public void RulesetDetailsPassTest()
        {
            mockContext.Setup(mock => mock.RuleSets().Find(RULESET_THREE)).Returns(rulesetWrapper);
            //mockRuleService.Setup(mock => mock.GetRulesetDetails(rulesetWrapper)).Returns(rulesetDetails);
            //Assert.AreEqual(ruleService.Details(RULESET_THREE), rulesetDetails);
        }

        [TestMethod]
        public void RulesetDetailsFailTest()
        {
            mockContext.Setup(mock => mock.RuleSets().Find(RULESET_THREE)).Returns((CardShop.Models.RuleSet)null);
            Assert.IsNull(ruleService.Details(RULESET_THREE));
            //mockContext.Verify();
        }

        //[TestMethod]
        //public void EditRulesetTest()
        //{
        //    mockRuleService.Setup(mock => mock.DeserializeJSONRulesObject("rules")).Returns(ruleObjects);
        //    mockRuleService.Setup(mock => mock.CompileRuleset(ruleset, ruleObjects)).Returns("rules");

        //    mockContext.Setup(mock => mock.RuleSets().Find(RULESET_THREE)).Returns(rulesetWrapper);

        //    Assert.AreEqual(ruleService.Edit(rulesetWrapper), rulesetWrapper);
        //    mockRuleService.Verify();
        //    mockContext.Verify(mock => mock.SaveChanges());
        //}
    }
}
