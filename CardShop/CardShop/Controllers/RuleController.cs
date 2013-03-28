using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Daos;
using CardShop.ViewModels;
using System.Workflow.Activities.Rules.Design;
using System.Web.Script.Serialization;
using System.Workflow.Activities.Rules;
using System.CodeDom;
using CardShop.Service;

namespace CardShop.Controllers
{
    public class RuleController : Controller
    {
        IPracticeGDVPDao db { get; set; }
        RuleService ruleService { get; set; }

        public RuleController()
        {
            db = PracticeGDVPDao.GetInstance();
            ruleService = new RuleService();
        }

        

        //
        // GET: /Rule/

        public ActionResult Index()
        {
            return View(db.RuleSets().ToList());
        }

        //
        // GET: /Rule/Details/5

        public ActionResult Details(int id = 0)
        {
            Models.RuleSet ruleset = db.RuleSets().Find(id);
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
            ruleService.Create(rulesetDetails.rulesetWrapper);
            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Models.RuleSet ruleset = db.RuleSets().Find(id);
            if (ruleset == null)
                return HttpNotFound();

            RulesetDetails model = new RulesetDetails(ruleset);
            return View(model);
        }

        //
        // POST: /Rule/Edit/5

        [HttpPost]
        public ActionResult CompileRules(RulesetDetails rulesetDetails)
        {
            ruleService.Edit(rulesetDetails.rulesetWrapper);
            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Models.RuleSet ruleset = db.RuleSets().Find(id);
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
            Models.RuleSet ruleset = db.RuleSets().Find(id);
            db.RuleSets().Remove(ruleset);
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