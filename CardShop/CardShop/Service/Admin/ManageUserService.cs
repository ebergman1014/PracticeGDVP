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
using CardShop.Utilities;


namespace CardShop.Service.Admin
{
    public class ManageUserService : IManageUserService
    {
        private static ManageUserService manageUserService;
        public IPracticeGDVPDao context { get; set; }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="userId">id of user to delete</param>
        /// <param name="isSuccess"> if Delete is sucessful, returns true</param>
        /// <returns></returns>
        public User DeleteUser(int userId, out bool isSuccess)
        {
            isSuccess = false;
            var user = context.Users().Find(userId);
            if (user != null)
            {
                user.IsActive = false;
                context.SaveChanges();
                isSuccess = true;
            }
            return user;
        }
        /// <summary>
        /// Gets all users 
        /// </summary>
        /// <param name="isSuccess">if success</param>
        /// <returns>a list of all users in Db</returns>
        public List<User> GetAllUsers(out bool isSuccess)
        {
            isSuccess = false;
            // might need to change to "IsActive"
            List<User> usersList = GetAllUsersList();
            // if users are returned, success!
            if (usersList.Count > 0)
            {
                isSuccess = true;
            }
            return usersList;
        }
        /// <summary>
        /// Get a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isSuccess"></param>
        /// <returns></returns>
        public User GetUser(int id, out bool isSuccess)
        {
            isSuccess = false;
            User user = context.Users().Find(id);
            if (user != null)
            {
                isSuccess = true;
            }
            return user;
        }
        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="user">User to create</param>
        /// <param name="isSuccess">if success</param>
        /// <returns></returns>
        public User CreateUser(User user, out bool isSuccess)
        {
            user.IsActive = true;
            context.Users().Add(user);
            context.SaveChanges();
            isSuccess = true;
            return user;

        }
        /// <summary>
        /// Keep to singletons, when we can..
        /// </summary>
        /// <returns> Singleton of ManageUserService()</returns>
        public static IManageUserService GetInstance()
        {
            if (manageUserService == null)
            {
                manageUserService = new ManageUserService();
            }
            return manageUserService;
        }
        /// <summary>
        /// Get RoleView, for @HTML select list
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <returns></returns>
        public IEnumerable GetRoleView(out bool isSuccess)
        {
            isSuccess = true;
            return context.webpages_Roles();
        }

        /// <summary>
        /// Edit a User
        /// </summary>
        /// <param name="user">User user</param>
        /// <param name="isSuccess">if success</param>
        /// <returns></returns>
        public User EditUser(User user, out bool isSuccess)
        {
            // isSuccess
            isSuccess = false;
            // grab user from Db
            User userToChange = context.Users().Find(user.UserId);
            if (userToChange != null)
            {
                // set properties for user
                userToChange.FirstName = user.FirstName;
                userToChange.RoleId = user.RoleId;
                userToChange.IsActive = user.IsActive;
                userToChange.LastName = user.LastName;
                userToChange.Email = user.Email;
                userToChange.IsActive = user.IsActive;
                // save changes to Db
                context.SaveChanges();

                isSuccess = true;
                // if userChanges is RoleId and succesful... 
                if (isSuccess && user.RoleId == (int)Role.StoreOwner)
                {
                    // see if we can creates a store for them!
                    // exciting!
                    CreateStore(user);
                }
            }
            return user;
        }
        /// <summary>
        /// Allows Admin to act as user
        /// </summary>
        /// <param name="id">id of user to act as</param>
        /// <param name="success">if succesful</param>
        public void ActAsUser(int id, out bool success)
        {
            User user = context.Users().Find(id);
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
        /// <summary>
        /// Turns off user
        /// </summary>
        /// <param name="success">set out parameter if is success</param>
        public void StopActingAsUser(out bool success)
        {
            UserAuth.Current.ActingAs = null;
            success = true;
        }
        /// <summary>
        /// Create a store, sets store owner as Owner, if person
        /// already owns a store, return null
        /// </summary>
        /// <param name="owner">User that is a StoreOwner</param>
        /// <returns>store created or null</returns>
        public Store CreateStore(User owner)
        {
            Store newStore = null;
            List<Store> store = FindStore(owner);
            // check that no stores are returned and owner.RoleId is still attached to the proper id#
            if (store.Count == 0 && owner.RoleId == (int)Role.StoreOwner)
            {
                // set newStore
                newStore = Factory.Instance.Create<Store>();
                // create store properties
                newStore.Name = owner.LastName + "'s";
                newStore.UserId = owner.UserId;
                context.Stores().Add(newStore);
                // save changes to objects tied to the context
                context.SaveChanges();
            }
            return newStore;
        }
        /// <summary>
        /// Finds all stores that is attached to the Owner.UserId == Store.UserId
        /// </summary>
        /// <param name="owner">User who is storeOwner</param>
        /// <returns></returns>
        public virtual List<Store> FindStore(User owner)
        {
            return context.Stores().Where(u => u.UserId == owner.UserId).ToList();
        }
        /// <summary>
        /// Returns a list of users, also brings back the webpages_Roles attached to user
        /// </summary>
        /// <returns></returns>
        public virtual List<User> GetAllUsersList()
        {
            return context.Users().Include(u => u.webpages_Roles).ToList();
        }
        /// <summary>
        /// No-Args constructor, set
        /// </summary>
        public ManageUserService()
        {
            context = PracticeGDVPDao.GetInstance();
        }

    }
}