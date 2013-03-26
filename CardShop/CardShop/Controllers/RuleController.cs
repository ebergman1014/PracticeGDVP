using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.DAL;
using CardShop.ViewModels;
using System.Workflow.Activities.Rules.Design;
using System.Web.Script.Serialization;
using System.Workflow.Activities.Rules;
using System.CodeDom;

namespace CardShop.Controllers
{
    public class RuleController : Controller
    {
        private RulesContext db = new RulesContext();

        private Models.RuleSet SetUpTest()
        {
            Models.RuleSet templateRuleset = new Models.RuleSet();
            templateRuleset.RuleSet1 = "<RuleSet Description=\"{p1:Null}\" Name=\"Template\" ChainingBehavior=\"Full\" xmlns:p1=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/workflow\">   <RuleSet.Rules>    <Rule Priority=\"0\" ReevaluationBehavior=\"Always\" Description=\"{p1:Null}\" Active=\"True\" Name=\"Test1\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"TestId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Field\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">50</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>    </RuleSet.Rules>  </RuleSet>";
            return templateRuleset;
        }

        //
        // GET: /Rule/

        public ActionResult Index()
        {
            return View(db.RuleSets.ToList());
        }

        //
        // GET: /Rule/Details/5

        public ActionResult Details(int id = 0)
        {
            Models.RuleSet ruleset = db.RuleSets.Find(id);
            if (ruleset == null)
                return HttpNotFound();

            RulesetDetails model = new RulesetDetails(ruleset);
            return View(model);
        }

        //
        // GET: /Rule/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Rule/Create

        [HttpPost]
        public ActionResult Create(RulesetDetails rulesetDetails)
        {
            Models.RuleSet rulesetWrapper = rulesetDetails.rulesetWrapper;
            rulesetWrapper.ModifiedDate = DateTime.Now;
            rulesetWrapper.MajorVersion = 1;
            rulesetWrapper.MinorVersion = 0;

            string updatedRules = rulesetDetails.rulesetWrapper.RuleSet1;
            List<RuleObject> rulesObject = DeserializeJSONRulesObject(updatedRules);

            Models.RuleSet templateRuleset = SetUpTest();
            System.Workflow.Activities.Rules.RuleSet ruleset = rulesetDetails.DeserializeRuleSet(templateRuleset.RuleSet1);
            ruleset.Name = rulesetWrapper.Name;

            rulesetWrapper.RuleSet1 = CompileRuleset(ruleset, rulesObject);

            db.RuleSets.Add(rulesetWrapper);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Models.RuleSet ruleset = db.RuleSets.Find(id);
            if (ruleset == null)
                return HttpNotFound();

            RulesetDetails model = new RulesetDetails(ruleset);
            return View(model);
        }

        //
        // POST: /Rule/Edit/5

        [HttpPost]
        public ActionResult CompileRules(FormCollection form)
        {
            Models.RuleSet rulesetWrapper = db.RuleSets.Find(Convert.ToInt32(form[0]));
            string rules = rulesetWrapper.RuleSet1;

            RulesetDetails rulesetDetails = new RulesetDetails();
            System.Workflow.Activities.Rules.RuleSet ruleset = rulesetDetails.DeserializeRuleSet(rules);

            string updatedRules = form["rulesObject"];
            List<RuleObject> rulesObject = DeserializeJSONRulesObject(updatedRules);

            string serializedRules = CompileRuleset(ruleset, rulesObject);

            rulesetWrapper.RuleSetId = Convert.ToInt32(form[0]);
            rulesetWrapper.Name = form[1];
            rulesetWrapper.MajorVersion = Convert.ToInt32(form[2]);
            rulesetWrapper.MinorVersion = Convert.ToInt32(form[3]);
            //rulesetWrapper.Status = Convert.ToInt16(form[4]);
            rulesetWrapper.AssemblyPath = form[5];
            rulesetWrapper.ActivityName = form[6];
            rulesetWrapper.ModifiedDate = DateTime.Now;
            rulesetWrapper.RuleSet1 = serializedRules;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Models.RuleSet ruleset = db.RuleSets.Find(id);
            if (ruleset == null)
            {
                return HttpNotFound();
            }
            return View(ruleset);
        }

        //
        // POST: /Rule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.RuleSet ruleset = db.RuleSets.Find(id);
            db.RuleSets.Remove(ruleset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // private rules methods

        private string CompileRuleset(System.Workflow.Activities.Rules.RuleSet ruleset, List<RuleObject> rulesObject)
        {
            System.Workflow.Activities.Rules.Rule rule = ruleset.Rules.ElementAt(0).Clone();
            RuleStatementAction action = (RuleStatementAction)rule.ThenActions.ElementAt(0).Clone();

            ruleset.Rules.Clear();

            foreach (var ruleObj in rulesObject)
            {
                System.Workflow.Activities.Rules.Rule thisRule = SetUpRule(rule, action, ruleObj);
                ruleset.Rules.Add(thisRule);
            }

            RulesetDetails rulesetDetails = new RulesetDetails();
            return rulesetDetails.SerializeRuleSet(ruleset);
        }

        private System.Workflow.Activities.Rules.Rule SetUpRule(System.Workflow.Activities.Rules.Rule rule, RuleStatementAction action, RuleObject ruleObj)
        {
            System.Workflow.Activities.Rules.Rule thisRule = rule.Clone();
            thisRule.ThenActions.Clear();
            thisRule.ElseActions.Clear();

            thisRule.Name = ruleObj.Name;

            thisRule.Condition = SetRuleCondition((RuleExpressionCondition)thisRule.Condition, ruleObj.Condition);

            foreach (var actionObj in ruleObj.ThenActions)
            {
                RuleStatementAction thisAction = (RuleStatementAction)action.Clone();
                thisAction = SetRuleAction(thisAction, actionObj);
                thisRule.ThenActions.Add(thisAction);
            }

            foreach (var actionObj in ruleObj.ElseActions)
            {
                action = SetRuleAction(action, actionObj);
                thisRule.ElseActions.Add(action);
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

        private List<RuleObject> DeserializeJSONRulesObject(string JSON)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<RuleObject> rulesObject = serializer.Deserialize<List<RuleObject>>(JSON);
            return rulesObject;
        }
    }
}