using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using CardShop.Models;
using CardShop.Service;

namespace CardShop.ViewModels
{
    public class RulesetDetails
    {
        public CardShop.Models.RuleSet rulesetWrapper { get; set; }
        public List<RuleObject> rulesDisplay { get; set; }

        public RulesetDetails()
        {
        }

        private List<RuleObject> SetUpRulesDisplay(ICollection<System.Workflow.Activities.Rules.Rule> rules)
        {
            List<RuleObject> rulesDisplay = new List<RuleObject>();

            foreach (System.Workflow.Activities.Rules.Rule rule in rules)
            {
                RuleObject ruleObj = new RuleObject(rule);
                rulesDisplay.Add(ruleObj);
            }

            return rulesDisplay;
        }

        public RulesetDetails getRulesetDetails(CardShop.Models.RuleSet rulesetWrapper)
        {
            RuleService ruleService = new RuleService();
            this.rulesetWrapper = rulesetWrapper;

            if (rulesetWrapper.RuleSet1 != null)
            {
                System.Workflow.Activities.Rules.RuleSet ruleset =
                    ruleService.DeserializeRules(rulesetWrapper.RuleSet1);
                ICollection<Rule> rules = ruleset.Rules;
                this.rulesDisplay = SetUpRulesDisplay(rules);
            }
            return this;
        }

    }
}