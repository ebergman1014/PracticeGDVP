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

namespace CardShop.Controllers
{
    public class RuleController : Controller
    {
        private RulesContext db = new RulesContext();
        private CardShop.Models.RuleSet testRuleset = new Models.RuleSet();

        private void SetUpTest()
        {
            testRuleset.MajorVersion = 1;
            testRuleset.MinorVersion = 0;
            testRuleset.ModifiedDate = DateTime.Now;
            testRuleset.Name = "Test";
            testRuleset.RuleSet1 = "<RuleSet Description=\"{p1:Null}\" Name=\"Test\" ChainingBehavior=\"Full\" xmlns:p1=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/workflow\">   <RuleSet.Rules>    <Rule Priority=\"0\" Description=\"{p1:Null}\" Active=\"True\" ReevaluationBehavior=\"Always\" Name=\"Region1\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"CustomerRegionId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">25</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Expiration\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2013-09-09</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>    <Rule Priority=\"0\" Description=\"{p1:Null}\" Active=\"True\" ReevaluationBehavior=\"Always\" Name=\"Region2\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"CustomerRegionId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">35</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Expiration\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2013-09-10</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>    <Rule Priority=\"0\" Description=\"{p1:Null}\" Active=\"True\" ReevaluationBehavior=\"Always\" Name=\"Region3\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">3</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"CustomerRegionId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">45</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Expiration\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2012-01-01</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>   </RuleSet.Rules>  </RuleSet>";
            testRuleset.Status = 0;
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
            //RuleSet ruleset = db.RuleSets.Find(id);
            //if (ruleset == null)
            //    return HttpNotFound();

            //RulesetDetails model = new RulesetDetails(ruleset);

            SetUpTest();
            RulesetDetails model = new RulesetDetails(testRuleset);

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
        [ValidateInput(false)]
        public ActionResult Create(Models.RuleSet ruleset)
        {
            //temporary create method
            if (ModelState.IsValid)
            {
                ruleset.MajorVersion = 1;
                ruleset.MinorVersion = 0;
                db.RuleSets.Add(ruleset);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(ruleset);
        }

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Create(RulesetDetails rulesetDetails)
        //{
        //    RuleSet rulesetWrapper = rulesetDetails.ruleset;
        //    rulesetWrapper.ModifiedDate = DateTime.Now;

        //    //here: set "RuleSet1" to serialized rulesetDetails.rules

        //    db.RuleSets.Add(rulesetWrapper);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //
        // GET: /Rule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //RuleSet ruleset = db.RuleSets.Find(id);
            //if (ruleset == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(ruleset);
            SetUpTest();
            RulesetDetails model = new RulesetDetails(testRuleset);
            return View(model);
        }

        //
        // POST: /Rule/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CompileRules(FormCollection form)
        {
            string rules = form[1];
            RulesetDetails rulesetDetails = new RulesetDetails();
            System.Workflow.Activities.Rules.RuleSet ruleset = rulesetDetails.DeserializeRuleSet(rules);

            string updatedRules = form["rulesObject"];
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<RulesObject> rulesObject = serializer.Deserialize<List<RulesObject>>(updatedRules);

            System.Workflow.Activities.Rules.Rule rule = ruleset.Rules.ElementAt(0).Clone();
            RuleAction action = rule.ThenActions.ElementAt(0).Clone();
            ruleset.Rules.Clear();

            foreach (var ruleObj in rulesObject)
            {
                System.Workflow.Activities.Rules.Rule thisRule = rule.Clone();
                RuleAction thisAction = action.Clone();

                thisRule.Condition.Name = ruleObj.Condition;

                //then and else actions need to be added to the rule here, but the RuleAction class is abstract.
                //not sure if its value can be overridden.
                foreach (var actionObj in ruleObj.ThenActions)
                {
                }

                foreach (var actionObj in ruleObj.ElseActions)
                {
                }
                
                
            }

            Models.RuleSet rulesetWrapper = new Models.RuleSet();
            rulesetWrapper.ActivityName = form["ActivityName"];
            rulesetWrapper.AssemblyPath = form["AssemblyPath"];
            rulesetWrapper.MajorVersion = Convert.ToInt32(form["MajorVersion"]);
            rulesetWrapper.MinorVersion = Convert.ToInt32(form["MinorVersion"]);
            rulesetWrapper.ModifiedDate = DateTime.Now;
            rulesetWrapper.Name = form["Name"];
            //rulesetWrapper.RuleSet1 = serializedRules;
            rulesetWrapper.RuleSetId = Convert.ToInt32(form["RuleSetId"]);
            rulesetWrapper.Status = Convert.ToInt16(form["Status"]);

            return RedirectToAction("Edit", rulesetWrapper);
        }

        [HttpPost]
        public ActionResult Edit(Models.RuleSet ruleset)
        {
            db.Entry(ruleset).State = EntityState.Modified;
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
    }
}