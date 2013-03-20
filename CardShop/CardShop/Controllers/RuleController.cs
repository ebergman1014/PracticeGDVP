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

namespace CardShop.Controllers
{
    public class RuleController : Controller
    {
        private RulesContext db = new RulesContext();

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

            RuleSet ruleset = new RuleSet();
            ruleset.MajorVersion = 1;
            ruleset.MinorVersion = 0;
            ruleset.ModifiedDate = DateTime.Now;
            ruleset.Name = "Test";
            ruleset.RuleSet1 = "<RuleSet Description=\"{p1:Null}\" Name=\"Test\" ChainingBehavior=\"Full\" xmlns:p1=\"http://schemas.microsoft.com/winfx/2006/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/workflow\">   <RuleSet.Rules>    <Rule Priority=\"0\" Description=\"{p1:Null}\" Active=\"True\" ReevaluationBehavior=\"Always\" Name=\"Region1\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">1</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"CustomerRegionId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">25</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Expiration\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2013-09-09</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>    <Rule Priority=\"0\" Description=\"{p1:Null}\" Active=\"True\" ReevaluationBehavior=\"Always\" Name=\"Region2\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"CustomerRegionId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">35</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Expiration\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2013-09-10</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>    <Rule Priority=\"0\" Description=\"{p1:Null}\" Active=\"True\" ReevaluationBehavior=\"Always\" Name=\"Region3\">     <Rule.Condition>      <RuleExpressionCondition Name=\"{p1:Null}\">       <RuleExpressionCondition.Expression>        <ns0:CodeBinaryOperatorExpression Operator=\"ValueEquality\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeBinaryOperatorExpression.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">3</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeBinaryOperatorExpression.Right>         <ns0:CodeBinaryOperatorExpression.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"CustomerRegionId\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeBinaryOperatorExpression.Left>        </ns0:CodeBinaryOperatorExpression>       </RuleExpressionCondition.Expression>      </RuleExpressionCondition>     </Rule.Condition>     <Rule.ThenActions>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Discount\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:Int32 xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">45</ns1:Int32>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>      <RuleStatementAction>       <RuleStatementAction.CodeDomStatement>        <ns0:CodeAssignStatement LinePragma=\"{p1:Null}\" xmlns:ns0=\"clr-namespace:System.CodeDom;Assembly=System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">         <ns0:CodeAssignStatement.Left>          <ns0:CodeFieldReferenceExpression FieldName=\"Expiration\">           <ns0:CodeFieldReferenceExpression.TargetObject>            <ns0:CodeThisReferenceExpression />           </ns0:CodeFieldReferenceExpression.TargetObject>          </ns0:CodeFieldReferenceExpression>         </ns0:CodeAssignStatement.Left>         <ns0:CodeAssignStatement.Right>          <ns0:CodePrimitiveExpression>           <ns0:CodePrimitiveExpression.Value>            <ns1:String xmlns:ns1=\"clr-namespace:System;Assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\">2012-01-01</ns1:String>           </ns0:CodePrimitiveExpression.Value>          </ns0:CodePrimitiveExpression>         </ns0:CodeAssignStatement.Right>        </ns0:CodeAssignStatement>       </RuleStatementAction.CodeDomStatement>      </RuleStatementAction>     </Rule.ThenActions>    </Rule>   </RuleSet.Rules>  </RuleSet>";
            ruleset.Status = 0;

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
        [ValidateInput(false)]
        public ActionResult Create(RuleSet ruleset)
        {
            //temporary create method
            if (ModelState.IsValid)
            {
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
            RuleSet ruleset = db.RuleSets.Find(id);
            if (ruleset == null)
            {
                return HttpNotFound();
            }
            return View(ruleset);
        }

        //
        // POST: /Rule/Edit/5

        [HttpPost]
        public ActionResult Edit(RuleSet ruleset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ruleset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ruleset);
        }

        //
        // GET: /Rule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RuleSet ruleset = db.RuleSets.Find(id);
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
            RuleSet ruleset = db.RuleSets.Find(id);
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