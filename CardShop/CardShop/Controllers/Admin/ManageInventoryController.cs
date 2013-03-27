using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Auth;
using CardShop.Daos;

namespace CardShop.Controllers.Admin
{
    public class ManageInventoryController : Controller
    {
        private IPracticeGDVPDao db = PracticeGDVPDao.GetInstance();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            IDbSet<StoreInventory> storeinventories = (IDbSet<StoreInventory>)db.StoreInventories().Include(s => s.BaseballCard).Include(s => s.Store);
            if (UserAuth.Current.HasRole(Role.StoreOwner))
            {
                storeinventories.Include(s => s.Store.UserId == UserAuth.Current.ActingUser.UserId); //Restrict to the current user's store.
            }
            return View(storeinventories.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            StoreInventory storeinventory = db.StoreInventories().Find(id);
            if (storeinventory == null)
            {
                return HttpNotFound();
            }
            return View(storeinventory);
        }

        //
        // GET: /Default1/Create

        public ActionResult Add()
        {
            ViewBag.BaseballCardId = new SelectList(db.BaseballCards(), "BaseballCardId", "Player");
            ViewBag.StoreId = new SelectList(db.Stores(), "StoreId", "Name");
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Add(StoreInventory storeinventory)
        {
            if (ModelState.IsValid)
            {
                db.StoreInventories().Add(storeinventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BaseballCardId = new SelectList(db.BaseballCards(), "BaseballCardId", "Player", storeinventory.BaseballCardId);
            ViewBag.StoreId = new SelectList(db.Stores(), "StoreId", "Name", storeinventory.StoreId);
            return View(storeinventory);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            StoreInventory storeinventory = db.StoreInventories().Find(id);
            if (storeinventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.BaseballCardId = new SelectList(db.BaseballCards(), "BaseballCardId", "Player", storeinventory.BaseballCardId);
            ViewBag.StoreId = new SelectList(db.Stores(), "StoreId", "Name", storeinventory.StoreId);
            return View(storeinventory);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(StoreInventory storeinventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storeinventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BaseballCardId = new SelectList(db.BaseballCards(), "BaseballCardId", "Player", storeinventory.BaseballCardId);
            ViewBag.StoreId = new SelectList(db.Stores(), "StoreId", "Name", storeinventory.StoreId);
            return View(storeinventory);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            StoreInventory storeinventory = db.StoreInventories().Find(id);
            if (storeinventory == null)
            {
                return HttpNotFound();
            }
            return View(storeinventory);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            StoreInventory storeinventory = db.StoreInventories().Find(id);
            db.StoreInventories().Remove(storeinventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}