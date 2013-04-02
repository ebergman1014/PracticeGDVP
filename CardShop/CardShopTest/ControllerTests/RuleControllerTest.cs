using System;
using System.Collections.Generic;
using CardShop.Models;
using CardShop.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CardShopTest.ControllerTests
{
    [TestClass]
    public class RuleControllerTest
    {

        Mock<IRuleService> mockRuleService;
        List<RuleSet> mockRuleSets;

        [TestMethod]
        public void TestIndexGetAllRuleSets()
        {

        }
    }
}
