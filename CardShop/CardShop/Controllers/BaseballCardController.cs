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

namespace CardShop.Controllers
{
    public class BaseballCardController : Controller
    {
        public IPracticeGDVPDao db { get; set; }
        UploadService uploadService { get; set; }

        public BaseballCardController()
        {
            db = PracticeGDVPDao.GetInstance();
            uploadService = new UploadService();
        }


        //
        // GET: /BaseballCard/

        public ActionResult Index()
        {
            return View(db.BaseballCards().ToList());
        }

        //
        // GET: /BaseballCard/Details/5

        public ActionResult Details(int id = 0)
        {
            BaseballCard baseballcard = db.BaseballCards().Find(id);
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
            return View("Upload");
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
                db.BaseballCards().Add(baseballcard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BaseballCard baseballcard = db.BaseballCards().Find(id);
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
                BaseballCard EditedCard = db.BaseballCards().Find(baseballcard.BaseballCardId);

                EditedCard.Player = baseballcard.Player;
                EditedCard.Team = baseballcard.Team;
                EditedCard.Cost = baseballcard.Cost;
                //db.Entry(baseballcard).State = EntityState.Modified;
                db.Entry(EditedCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BaseballCard baseballcard = db.BaseballCards().Find(id);
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
            BaseballCard baseballcard = db.BaseballCards().Find(id);
            db.BaseballCards().Remove(baseballcard);
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