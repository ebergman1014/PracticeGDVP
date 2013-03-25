using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Daos;
using CardShop.Models;
using System.Data.Entity;
using System.Collections;
using System.Data;
using CardShop.Auth;
using System.Data.Objects;


namespace CardShop.Service.Admin
{
    public class ManageUserService : IManageUserService
    {
        private static ManageUserService manageUserService;
        IPracticeGDVPDao db { get; set; }

        public User DeleteUser(int id, out bool isSuccess)
        {
            isSuccess = false;
            User user = db.Users().Find(id);
            if (user != null)
            {
                db.Users().Remove(user);
                isSuccess = true;
                db.SaveChanges();
            }
            return user;
        }

        public List<User> GetAllUsers(out bool isSuccess)
        {
            isSuccess = false;
            PracticeGDVPEntities idb = new PracticeGDVPEntities();
            var user = db.Users().Include(u => u.webpages_Roles).ToList();
            if (user.Count > 0)
            {
                isSuccess = true;
            }
            return user.ToList();
        }

        public User GetUser(int id, out bool isSuccess)
        {
            isSuccess = false;
            User user = db.Users().Find(id);
            if (user != null)
            {
                isSuccess = true;
            }
            return user;
        }

        public User CreateUser(User user, out bool isSuccess)
        {

            db.Users().Add(user);
            db.SaveChanges();
            isSuccess = true;
            return user;

        }


        private ManageUserService()
        {
            db = PracticeGDVPDao.GetInstance();
        }

        public static IManageUserService GetInstance()
        {
            if (manageUserService == null)
            {
                manageUserService = new ManageUserService();
            }
            return manageUserService;
        }


        public IEnumerable GetRoleView(out bool isSuccess)
        {
            isSuccess = true;
            return db.webpages_Roles();
        }


        public User EditUser(User user, out bool isSuccess)
        {
            var aUser = db.Users().Find(user.UserId);
            if (aUser != null)
            {
                aUser.FirstName = user.FirstName;
                aUser.RoleId = user.RoleId;
                aUser.IsActive = user.IsActive;
                aUser.LastName = user.LastName;
                aUser.Email = user.Email;

                db.SaveChanges();
                isSuccess = true;
            }
            else
            {
                isSuccess = false;
            }
            return user;
        }
        public void ActAsUser(int id, out bool success)
        {
            User user = db.Users().Find(id);
            if (user != null)
            {
                UserAuth.Current.ActingAs = user;
                success = true;
            }
            else
            {
                success = false;
            }
        }

        public void StopActingAsUser(out bool success)
        {
            UserAuth.Current.ActingAs = null;
            success = true;
        }
    }
}