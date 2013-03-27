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
        public IPracticeGDVPDao db { get; set; }

        public User DeleteUser(int id, out bool isSuccess)
        {
            isSuccess = false;
            var user = db.Users().Find(id);
            if (user != null)
            {
                user.IsActive = false;
                db.SaveChanges();
                isSuccess = true;
            }
            return user;
        }

        public List<User> GetAllUsers(out bool isSuccess)
        {
            isSuccess = false;
            var user = db.Users().Include(u => u.webpages_Roles);
            List<User> usersList = user.ToList();
            if (usersList.Count > 0)
            {
                isSuccess = true;
            }
            return usersList;
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
            user.IsActive = true;
            db.Users().Add(user);
            db.SaveChanges();
            isSuccess = true;
            return user;

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
                aUser.IsActive = user.IsActive;

                db.SaveChanges();
                isSuccess = true;
                if (isSuccess && user.RoleId == (int)Role.StoreOwner)
                {
                    CreateStore(user.UserId);
                }
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

        private ManageUserService()
        {
            db = PracticeGDVPDao.GetInstance();
        }

        public void CreateStore(int ownerId)
        {
            List<Store> store = db.Stores().Where(u => u.UserId == ownerId).ToList();

            if (store.Count == 0)
            {
                var newStore = db.Stores().Create();
                newStore.UserId = ownerId;
                newStore.Name = "NEW STORE";
                db.SaveChanges();
            }
        }

    }
}