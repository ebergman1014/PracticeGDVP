using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;

namespace CardShop.Controllers
{
    public class ManageUserController : Controller
    {
        private PracticeGDVPEntities db = new PracticeGDVPEntities();

        //
        // GET: /ManageUser/

        public ActionResult Index()
        {
            var user = db.User.Include(u => u.webpages_Roles);
            return View(user.ToList());
        }

        //
        // GET: /ManageUser/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /ManageUser/Create

        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.webpages_Roles, "RoleId", "RoleName");
            return View();
        }

        //
        // POST: /ManageUser/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.webpages_Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        //
        // GET: /ManageUser/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.webpages_Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        //
        // POST: /ManageUser/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.webpages_Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        //
        // GET: /ManageUser/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /ManageUser/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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