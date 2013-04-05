using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Workflow.Activities.Rules;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using CardShop.Daos;
using CardShop.ViewModels;

namespace CardShop.Service
{
    public class RuleService
    {
        #region Properties and Constructors

        public IPracticeGDVPDao dbContext { get; set; }
        public System.Workflow.Activities.Rules.RuleSet templateRuleset { get; set; }

        public RuleService()
        {
            this.dbContext = PracticeGDVPDao.GetInstance();
            this.templateRuleset = SetUpTemplate();
        }

        #endregion

        #region CRUD Main Methods

        public Models.RuleSet Create(Models.RuleSet rulesetWrapper)
        {
            rulesetWrapper.ModifiedDate = DateTime.Now;
            rulesetWrapper.MajorVersion = 1;
            rulesetWrapper.MinorVersion = 0;

            string updatedRules = rulesetWrapper.RuleSet1;
            List<RuleObject> rulesObject = DeserializeJSONRulesObject(updatedRules);

            System.Workflow.Activities.Rules.RuleSet ruleset = templateRuleset.Clone();
            ruleset.Name = rulesetWrapper.Name;

            rulesetWrapper.RuleSet1 = CompileRuleset(ruleset, rulesObject);

            dbContext.RuleSets().Add(rulesetWrapper);
            dbContext.SaveChanges();

            return rulesetWrapper;
        }

        public Models.RuleSet Edit(Models.RuleSet rulesetWrapper)
        {
            string rules = rulesetWrapper.RuleSet1;
            List<RuleObject> rulesObject = DeserializeJSONRulesObject(rules);

            string serializedRules = CompileRuleset(templateRuleset, rulesObject);

            Models.RuleSet dbRuleset = dbContext.RuleSets().Where(p => p.RuleSetId == rulesetWrapper.RuleSetId).FirstOrDefault();
            dbRuleset.ActivityName = rulesetWrapper.ActivityName;
            dbRuleset.AssemblyPath = rulesetWrapper.AssemblyPath;
            dbRuleset.MajorVersion = rulesetWrapper.MajorVersion;
            dbRuleset.MinorVersion = rulesetWrapper.MinorVersion;
            dbRuleset.ModifiedDate = DateTime.Now;
            dbRuleset.Name = rulesetWrapper.Name;
            dbRuleset.RuleSet1 = serializedRules;
            dbRuleset.Status = rulesetWrapper.Status;

            //dbRuleset = rulesetWrapper;
            //dbContext.Entry(dbRuleset).CurrentValues.SetValues(rulesetWrapper);
            //dbContext.Entry(dbRuleset).State = EntityState.Modified;

            dbContext.SaveChanges();

            return rulesetWrapper;
        }

        public Models.RuleSet Details(int id)
        {
            Models.RuleSet ruleset = dbContext.RuleSets().Find(id);
            if (ruleset != null)
                return ruleset;

            return null;
        }

        public void Delete(int id)
        {
            Models.RuleSet ruleset = dbContext.RuleSets().Find(id);
            if (ruleset != null)
            {
                dbContext.RuleSets().Remove(ruleset);
                dbContext.SaveChanges();
            }
        }

        #endregion

        #region Batch Upload

        public List<Models.RuleSet> Upload(string filename)
        {
            Models.RuleSet ruleset;
            List<Models.RuleSet> allRulesets = new List<Models.RuleSet>();

            if (File.Exists(filename))
            {
                using (FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (!String.IsNullOrEmpty(line))
                            {
                                ruleset = new Models.RuleSet();
                                string[] lineArray = Regex.Split(line, "; ");

                                try
                                {
                                    ruleset.Name = lineArray[0];
                                    ruleset.ActivityName = lineArray[1];
                                    ruleset.AssemblyPath = lineArray[2];
                                    ruleset.RuleSet1 = lineArray[3];
                                    ruleset.MajorVersion = 1;
                                    ruleset.MinorVersion = 0;
                                    ruleset.ModifiedDate = DateTime.Now;

                                    allRulesets.Add(ruleset);
                                }
                                catch (FormatException ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
                return SaveImport(allRulesets);
            }
            return null;
        }

        private List<Models.RuleSet> SaveImport(List<Models.RuleSet> rulesets)
        {
            List<Models.RuleSet> savedRulesets = new List<Models.RuleSet>();
            foreach (Models.RuleSet ruleset in rulesets)
            {
                dbContext.RuleSets().Add(ruleset);
                savedRulesets.Add(ruleset);
            }
            dbContext.SaveChanges();
            return savedRulesets;
        }

        #endregion

        #region Serialization Methods

        public System.Workflow.Activities.Rules.RuleSet DeserializeRules(string rules)
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
                }
            }

            return ruleDefinition.ToString();
        }

        private List<RuleObject> DeserializeJSONRulesObject(string JSON)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<RuleObject> rulesObject = serializer.Deserialize<List<RuleObject>>(JSON);
            return rulesObject;
        }

        #endregion

        #region Ruleset Compilation

        private System.Workflow.Activities.Rules.RuleSet SetUpTemplate()
        {
            Models.RuleSet rulesetWrapper = new Models.RuleSet();
            rulesetWrapper.RuleSet1 = "<RuleSet Description=\"{p1:Null}\" Name=\"Template\" ChainingBehavior=\"Full\" xmlns:p1=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/workflow\">   <RuleSet.Rules>    <Rule Priority=\"0\" ReevaluationBehavior=\"Always\" Description=\"{p1:Null}\" Active=\"True\" Name=\"Test1\"><Rule.Condition><RuleExpressionCondition Name=\"{p1:Null}\"><RuleExpressionCondition.Expression><ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"><ns0:CodeBinaryOperatorExpression.Right><ns0:CodePrimitiveExpression><ns0:CodePrimitiveExpression.Value><ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:String></ns0:CodePrimitiveExpression.Value></ns0:CodePrimitiveExpression></ns0:CodeBinaryOperatorExpression.Right><ns0:CodeBinaryOperatorExpression.Left><ns0:CodePropertyReferenceExpression PropertyName=\"TestId\"><ns0:CodePropertyReferenceExpression.TargetObject><ns0:CodeThisReferenceExpression /></ns0:CodePropertyReferenceExpression.TargetObject></ns0:CodePropertyReferenceExpression></ns0:CodeBinaryOperatorExpression.Left></ns0:CodeBinaryOperatorExpression></RuleExpressionCondition.Expression></RuleExpressionCondition></Rule.Condition><Rule.ThenActions><RuleStatementAction><RuleStatementAction.CodeDomStatement><ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"><ns0:CodeAssignStatement.Left><ns0:CodePropertyReferenceExpression PropertyName=\"Field\"><ns0:CodePropertyReferenceExpression.TargetObject><ns0:CodeThisReferenceExpression /></ns0:CodePropertyReferenceExpression.TargetObject></ns0:CodePropertyReferenceExpression></ns0:CodeAssignStatement.Left><ns0:CodeAssignStatement.Right><ns0:CodePrimitiveExpression><ns0:CodePrimitiveExpression.Value><ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:String></ns0:CodePrimitiveExpression.Value></ns0:CodePrimitiveExpression></ns0:CodeAssignStatement.Right></ns0:CodeAssignStatement></RuleStatementAction.CodeDomStatement></RuleStatementAction><RuleStatementAction><RuleStatementAction.CodeDomStatement><ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"><ns0:CodeAssignStatement.Left><ns0:CodePropertyReferenceExpression PropertyName=\"Discount\"><ns0:CodePropertyReferenceExpression.TargetObject><ns0:CodeThisReferenceExpression /></ns0:CodePropertyReferenceExpression.TargetObject></ns0:CodePropertyReferenceExpression></ns0:CodeAssignStatement.Left><ns0:CodeAssignStatement.Right><ns0:CodePrimitiveExpression><ns0:CodePrimitiveExpression.Value><ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">50</ns1:String></ns0:CodePrimitiveExpression.Value></ns0:CodePrimitiveExpression></ns0:CodeAssignStatement.Right></ns0:CodeAssignStatement></RuleStatementAction.CodeDomStatement></RuleStatementAction></Rule.ThenActions></Rule></RuleSet.Rules></RuleSet>";
            System.Workflow.Activities.Rules.RuleSet ruleset = DeserializeRules(rulesetWrapper.RuleSet1);
            return ruleset;
        }

        private string CompileRuleset(System.Workflow.Activities.Rules.RuleSet ruleset, List<RuleObject> rulesObject)
        {
            System.Workflow.Activities.Rules.Rule rule = ruleset.Rules.ElementAt(0).Clone();
            RuleStatementAction action = (RuleStatementAction)rule.ThenActions.ElementAt(0).Clone();

            ruleset.Rules.Clear();

            foreach (RuleObject ruleObj in rulesObject)
            {
                System.Workflow.Activities.Rules.Rule thisRule = SetUpRule(rule, action, ruleObj);
                ruleset.Rules.Add(thisRule);
            }

            RulesetDetails rulesetDetails = new RulesetDetails();
            return SerializeRuleSet(ruleset);
        }

        private System.Workflow.Activities.Rules.Rule SetUpRule(System.Workflow.Activities.Rules.Rule rule, RuleStatementAction action, RuleObject ruleObj)
        {
            System.Workflow.Activities.Rules.Rule thisRule = rule.Clone();
            thisRule.ThenActions.Clear();
            thisRule.ElseActions.Clear();

            thisRule.Name = ruleObj.Name;

            thisRule.Condition = SetRuleCondition((RuleExpressionCondition)thisRule.Condition, ruleObj.Condition);

            foreach (ActionObject actionObj in ruleObj.ThenActions)
            {
                RuleStatementAction thisAction = (RuleStatementAction)action.Clone();
                thisAction = SetRuleAction(thisAction, actionObj);
                thisRule.ThenActions.Add(thisAction);
            }

            foreach (ActionObject actionObj in ruleObj.ElseActions)
            {
                RuleStatementAction thisAction = (RuleStatementAction)action.Clone();
                thisAction = SetRuleAction(thisAction, actionObj);
                thisRule.ElseActions.Add(thisAction);
            }

            return thisRule;
        }

        private RuleExpressionCondition SetRuleCondition(RuleExpressionCondition condition, ConditionObject conditionObj)
        {
            CodeBinaryOperatorExpression operatorExpression = (CodeBinaryOperatorExpression)condition.Expression;

            CodeFieldReferenceExpression leftExpression = (CodeFieldReferenceExpression)operatorExpression.Left;
            leftExpression.FieldName = conditionObj.field;

            CodePrimitiveExpression rightExpression = (CodePrimitiveExpression)operatorExpression.Right;
            rightExpression.Value = conditionObj.value;

            return condition;
        }

        private RuleStatementAction SetRuleAction(RuleStatementAction action, ActionObject actionObj)
        {
            CodeAssignStatement domDataValue = (CodeAssignStatement)action.CodeDomStatement;

            CodeFieldReferenceExpression expressionField = (CodeFieldReferenceExpression)domDataValue.Left;
            expressionField.FieldName = actionObj.field;

            CodePrimitiveExpression expressionValue = (CodePrimitiveExpression)domDataValue.Right;
            expressionValue.Value = actionObj.value;

            return action;
        }

        #endregion

    }
}