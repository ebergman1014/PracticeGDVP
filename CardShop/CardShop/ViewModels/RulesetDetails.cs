using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using CardShop.Models;

namespace CardShop.ViewModels
{
    public class RulesetDetails
    {
        public CardShop.Models.RuleSet ruleset;
        public ICollection<Rule> rules;

        public RulesetDetails(CardShop.Models.RuleSet ruleset)
        {
            this.ruleset = ruleset;
            if (ruleset.RuleSet1 != null)
                this.rules = DeserializeRuleSet(ruleset.RuleSet1).Rules;
        }

        public RulesetDetails()
        {
        }

        private System.Workflow.Activities.Rules.RuleSet DeserializeRuleSet(string rules)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            System.Workflow.Activities.Rules.RuleSet ruleset = (System.Workflow.Activities.Rules.RuleSet)serializer.Deserialize(XmlReader.Create(new StringReader(rules)));
            return ruleset;
        }

        private string SerializeRuleSet(System.Workflow.Activities.Rules.RuleSet ruleset)
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
    }
}