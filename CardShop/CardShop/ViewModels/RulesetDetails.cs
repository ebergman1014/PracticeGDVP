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

namespace CardShop.ViewModels
{
    public class RulesetDetails
    {
        public CardShop.Models.RuleSet rulesetWrapper { get; set; }
        public System.Workflow.Activities.Rules.RuleSet ruleset { get; set; }
        public ICollection<Rule> rules { get; set; }
        public List<RuleObject> rulesDisplay { get; set; }

        public RulesetDetails(CardShop.Models.RuleSet rulesetWrapper)
        {
            this.rulesetWrapper = rulesetWrapper;
            if (rulesetWrapper.RuleSet1 != null)
            {
                this.ruleset = DeserializeRuleSet(rulesetWrapper.RuleSet1);
                this.rules = ruleset.Rules;
                this.rulesDisplay = SetUpRulesDisplay(rules);
            }
        }

        public RulesetDetails()
        {
        }

        public System.Workflow.Activities.Rules.RuleSet DeserializeRuleSet(string rules)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            System.Workflow.Activities.Rules.RuleSet ruleset = (System.Workflow.Activities.Rules.RuleSet)serializer.Deserialize(XmlReader.Create(new StringReader(rules)));
            return ruleset;
        }

        public string SerializeRuleSet(System.Workflow.Activities.Rules.RuleSet ruleset)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            StringBuilder ruleDefinition = new StringBuilder();

            if (ruleset != null)
            {
                try
                {
                    StringWriter stringWriter = new StringWriter(ruleDefinition);
                    XmlTextWriter writer = new XmlTextWriter(stringWriter);
                    serializer.Serialize(writer, ruleset);
                    writer.Flush();
                    writer.Close();
                    stringWriter.Flush();
                    stringWriter.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return ruleDefinition.ToString();
        }

        public List<RuleObject> SetUpRulesDisplay(ICollection<System.Workflow.Activities.Rules.Rule> rules)
        {
            List<RuleObject> rulesDisplay = new List<RuleObject>();

            foreach (System.Workflow.Activities.Rules.Rule rule in rules)
            {
                RuleObject ruleObj = new RuleObject(rule);
                rulesDisplay.Add(ruleObj);
            }

            return rulesDisplay;
        }
    }
}