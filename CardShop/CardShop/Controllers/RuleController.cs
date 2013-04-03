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
using System.CodeDom;
using CardShop.Service;
using System.IO;

namespace CardShop.Controllers
{
    public class RuleController : Controller
    {
        private IRuleService ruleService;

        public RuleController(IRuleService service)
        {
            ruleService = service;
        }

        //
        // GET: /Rule/
        public ActionResult Index()
        {
            return View(ruleService.GetAllRulesets());
        }

        //
        // GET: /Rule/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        //
        // POST: /Rule/Create
        [HttpPost]
        public ActionResult Create(RulesetDetails rulesetDetails)
        {
            //This needs some form of confirmation
            ruleService.Create(rulesetDetails.rulesetWrapper);
            return RedirectToAction("Index");
        }

        //
        // GET: /Rule/Edit/5
        public ActionResult Edit(int id = 0)
        {
            RulesetDetails ruleset = ruleService.Details(id);
            if (ruleset == null)
                return HttpNotFound();

            return View(ruleset);
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
        // GET: /Rule/Details/5
        public ActionResult Details(int id = 0)
        {
            RulesetDetails ruleset = ruleService.Details(id);
            if (ruleset == null)
                return HttpNotFound();

            return View(ruleset);
        }

        //
        // GET: /Rule/Delete/5
        public ActionResult Delete(int id = 0)
        {
            RulesetDetails ruleset = ruleService.Details(id);
            if (ruleset == null)
                return HttpNotFound();

            return View(ruleset);
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