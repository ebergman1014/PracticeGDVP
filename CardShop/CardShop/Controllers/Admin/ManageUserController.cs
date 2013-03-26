using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Models;
using CardShop.Controllers.Admin;
using CardShop.Service;
using CardShop.Service.Admin;
using CardShop.Auth;
using CardShop.Daos;
using CardShop.Auth;

namespace CardShop.Controllers
{
    [AuthorizeUser(Role.Admin, Role.StoreOwner)]
    public class ManageUserController : Controller, IManageUserController
    {
        public IManageUserService manageUserService { get; set; }
        public IMembership membership { get; set; }
        //
        // GET: /ManageUser/
        public ActionResult Index()
        {
            bool isSuccess;
            return View(manageUserService.GetAllUsers(out isSuccess));
        }

        //
        // GET: /ManageUser/Details/5

        public ActionResult Details(int id = 0)
        {
            bool isSuccess;
            User user = manageUserService.GetUser(id, out isSuccess);
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
            bool isSuccess;
            ViewBag.RoleId = new SelectList(manageUserService.GetRoleView(out isSuccess), "RoleId", "RoleName");
            return View();
        }

        //
        // POST: /ManageUser/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            bool isSuccess;
            if (ModelState.IsValid)
            {
                manageUserService.CreateUser(user, out isSuccess);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(manageUserService.GetRoleView(out isSuccess), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        //
        // GET: /ManageUser/Edit/5

        public ActionResult Edit(int id = 0)
        {
            bool isSuccess;
            User user = manageUserService.GetUser(id, out isSuccess);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(manageUserService.GetRoleView(out isSuccess), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        //
        // POST: /ManageUser/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            bool isSuccess;
            if (ModelState.IsValid)
            {
                manageUserService.EditUser(user, out isSuccess);
                
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(manageUserService.GetRoleView(out isSuccess), "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        //
        // GET: /ManageUser/Delete/5

        public ActionResult Delete(int id = 0)
        {
            bool isSuccess;
            User user = manageUserService.GetUser(id, out isSuccess);
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
            bool isSuccess = false;
            manageUserService.DeleteUser(id, out isSuccess);

            return RedirectToAction("Index");
        }

        public ManageUserController()
        {
            manageUserService = ManageUserService.GetInstance();
        }

        [HttpGet, ActionName("ActAsUser")]
        [AuthorizeUser(Role.Admin, Role.StoreOwner)]
        public ActionResult ActAsUser(int id)
        {
            bool success;
            manageUserService.ActAsUser(id, out success);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet, ActionName("StopActingAsUser")]
        [AuthorizeUser]
        public ActionResult StopActingAsUser()
        {
            bool success;
            manageUserService.StopActingAsUser(out success);
            return RedirectToAction("Index", "Home");
        }
    }
}