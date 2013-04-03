using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Workflow.ComponentModel.Compiler;
using System.Diagnostics;

namespace Microsoft.Rules.Samples.ExternalRuleSetToolkit
{
    public class WorkflowHelper
    {
        public Dictionary<string, RuleSet> ruleCache;
        public WorkflowHelper()
        {
            ruleCache = new Dictionary<string, RuleSet>();
        }

        private static RuleSet GetRuleSetFromDB(string ruleSetName)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RuleSetStore"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT TOP 1 [RuleSet] FROM RuleSet WHERE Name=@name ORDER BY MajorVersion DESC , MinorVersion DESC", conn))
                {
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 128);
                    cmd.Parameters["@name"].Value = ruleSetName;
                    conn.Open();

                    object rulesObj = cmd.ExecuteScalar();

                    if (rulesObj is DBNull || rulesObj == null)
                        return null;

                    string rules = rulesObj.ToString();
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    RuleSet ruleset = (RuleSet)serializer.Deserialize(XmlReader.Create(new StringReader(rules)));
                    return ruleset;
                }
            }
        }

        public RuleSet GetRules(string ruleSetName)
        {
            if (ruleCache.ContainsKey(ruleSetName))
                return ruleCache[ruleSetName];
            else
            {
                RuleSet rules = GetRuleSetFromDB(ruleSetName);
                ruleCache[ruleSetName] = rules;
                return rules;
            }
        }

        public void RunRules<T>(T target, string rulesName)
        {
            RuleSet rules = GetRules(rulesName);
            if (rules != null)
            {
                RuleEngine engine = new RuleEngine(rules, typeof(T));
                engine.Execute(target);
            }
        }
    }
}
