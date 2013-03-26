using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Daos;

namespace CardShop.Controllers
{
    public class BaseballCardController : Controller
    {
        private DefaultContext db = new DefaultContext();

        //
        // GET: /BaseballCard/

        public ActionResult Index()
        {
            return View(db.BaseballCards.ToList());
        }

        //
        // GET: /BaseballCard/Details/5

        public ActionResult Details(int id = 0)
        {
            BaseballCard baseballcard = db.BaseballCards.Find(id);
            if (baseballcard == null)
            {
                return HttpNotFound();
            }
            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Upload

        public ActionResult Upload()
        {
            return View();
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
                db.BaseballCards.Add(baseballcard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Edit/5

        public ActionResult Edit(int id = 0)
        {
            BaseballCard baseballcard = db.BaseballCards.Find(id);
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
                db.Entry(baseballcard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baseballcard);
        }

        //
        // GET: /BaseballCard/Delete/5

        public ActionResult Delete(int id = 0)
        {
            BaseballCard baseballcard = db.BaseballCards.Find(id);
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
            BaseballCard baseballcard = db.BaseballCards.Find(id);
            db.BaseballCards.Remove(baseballcard);
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