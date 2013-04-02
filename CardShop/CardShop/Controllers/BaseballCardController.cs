using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Daos;
using System.IO;
using CardShop.Service;
using Microsoft.Practices.Unity;

namespace CardShop.Controllers
{
    public class BaseballCardController : Controller
    {
        public UploadService uploadService { get; set; }
        private IBaseballCardService baseballCardService;

        public BaseballCardController(IBaseballCardService service)
        {
            baseballCardService = service;
        }

        //
        // GET: /BaseballCard/
        public ActionResult Index()
        {
            return View(baseballCardService.GetAllCards());
        }

        //
        // GET: /BaseballCard/Details/5

        public ActionResult Details(int id = 0)
        {
            BaseballCard baseballcard = baseballCardService.GetBaseballCard(id);
            if (baseballcard == null)
            {
                return HttpNotFound();
            }
            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Browse

        public ActionResult Browse()
        {
            return View("Browse");
        }

        //
        // GET: /BaseballCard/Upload

        public ActionResult Upload()
        {
            return View();
        }

        //
        // POST: /BaseballCard/Upload

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

                    List<BaseballCard> savedCards = uploadService.LoadFromFile(path);
                    TempData["cards"] = savedCards.Count + " cards added.";
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Please select a file.";
                return View("Upload");
            }

        }

        //
        // GET: /BaseballCard/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BaseballCard/Create

        [HttpPost]
        public ActionResult Create(BaseballCard baseballcard)
        {
            if (ModelState.IsValid)
            {
                baseballCardService.Create(baseballcard);
                baseballCardService.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BaseballCard baseballcard = baseballCardService.GetBaseballCard(id);
            if (baseballcard == null)
            {
                return HttpNotFound();
            }
            return View(baseballcard);
        }

        //
        // POST: /BaseballCard/Edit/5

        [HttpPost]
        public ActionResult Edit(BaseballCard baseballcard)
        {
            if (ModelState.IsValid)
            {
                BaseballCard EditedCard = baseballCardService.GetBaseballCard(baseballcard.BaseballCardId);

                EditedCard.Player = baseballcard.Player;
                EditedCard.Team = baseballcard.Team;
                EditedCard.Cost = baseballcard.Cost;
                baseballCardService.Update(EditedCard);
                baseballCardService.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("error", "There has been an error, please try again or contact System Administrator.");
            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BaseballCard baseballcard = baseballCardService.GetBaseballCard(id);
            if (baseballcard == null)
            {
                return HttpNotFound();
            }
            return View(baseballcard);
        }

        //
        // POST: /BaseballCard/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BaseballCard baseballcard = baseballCardService.GetBaseballCard(id);
            baseballCardService.Delete(baseballcard.BaseballCardId);
            baseballCardService.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}