using System;
using System.Collections.Generic;
using System.Linq;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Compiler;
using CardShop.Daos;
using CardShop.Models;

namespace CardShop.Service.Rules
{
    public class RuleEngineService
    {
        private RuleService ruleService { get; set; }
        public Dictionary<int, System.Workflow.Activities.Rules.RuleSet> ruleIdCache { get; set; }
        public Dictionary<string, System.Workflow.Activities.Rules.RuleSet> ruleNameCache { get; set; }
        private IPracticeGDVPDao dbContext { get; set; }

        public RuleEngineService()
        {
            ruleService = new RuleService();
            ruleIdCache = new Dictionary<int, System.Workflow.Activities.Rules.RuleSet>();
            ruleNameCache = new Dictionary<string, System.Workflow.Activities.Rules.RuleSet>();
            dbContext = PracticeGDVPDao.GetInstance();
        }

        public System.Workflow.Activities.Rules.RuleSet GetRulesById(int id)
        {
            if (ruleIdCache.ContainsKey(id))
                return ruleIdCache[id];
            else
            {
                CardShop.Models.RuleSet rulesetWrapper = dbContext.RuleSets().Find(id);
                System.Workflow.Activities.Rules.RuleSet ruleset = ruleService.DeserializeRules(rulesetWrapper.RuleSet1);
                ruleIdCache[id] = ruleset;
                return ruleset;
            }
        }

        public System.Workflow.Activities.Rules.RuleSet GetRulesByName(string name)
        {
            if (ruleNameCache.ContainsKey(name))
            {
                return ruleNameCache[name];
            }
            else
            {
                CardShop.Models.RuleSet rulesetWrapper = dbContext.RuleSets().Where(p => p.Name == name).FirstOrDefault();
                System.Workflow.Activities.Rules.RuleSet ruleset = ruleService.DeserializeRules(rulesetWrapper.RuleSet1);
                ruleNameCache[name] = ruleset;
                return ruleset;
            }
        }

        public void RunRules<T>(T target, System.Workflow.Activities.Rules.RuleSet ruleset)
        {
            if (ruleset != null)
            {
                RuleEngine engine = new RuleEngine(ruleset, typeof(T));
                engine.Execute(target);
            }
        }

    }
}
