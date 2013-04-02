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
using System.IO;

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
            Models.RuleSet ruleset = ruleService.Details(id);
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
            Models.RuleSet ruleset = ruleService.Details(id);
            if (ruleset == null)
                return HttpNotFound();

            RulesetDetails model = new RulesetDetails(ruleset);
            return View(model);
        }

        //
        // POST: /Rule/Edit/5

        [HttpPost]
        public ActionResult Edit(RulesetDetails rulesetDetails)
        {
            ruleService.Edit(rulesetDetails.rulesetWrapper);
            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Models.RuleSet ruleset = ruleService.Details(id);
            if (ruleset == null)
                return HttpNotFound();

            RulesetDetails model = new RulesetDetails(ruleset);
            return View(model);
        }

        //
        // POST: /Rule/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ruleService.Delete(id);
            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Upload

        public ActionResult Upload()
        {
            return View();
        }

        //
        // POST: /Rule/Upload

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);

                    List<Models.RuleSet> savedRulesets = ruleService.Upload(path);
                    TempData["rules"] = savedRulesets.Count + " rules added.";
                }
            }
            return RedirectToAction("Index");
        }
    }
}