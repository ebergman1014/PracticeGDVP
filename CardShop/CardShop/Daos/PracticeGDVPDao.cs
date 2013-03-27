using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CardShop.Utilities;

namespace CardShop.Daos
{
    public class PracticeGDVPDao : IPracticeGDVPDao
    {
        private PracticeGDVPEntities gdvp;
        private IDbSet<User> users;
        private IDbSet<BaseballCard> baseBallCards;
        private IDbSet<BaseballCardTransaction> baseballCardTransactions;
        private IDbSet<RuleSet> ruleSets;
        private IDbSet<StoreInventory> storeInventories;
        private IDbSet<Transaction> transactions;
        private IDbSet<Store> stores;
        private IDbSet<UserDiscount> userDiscounts;
        public IDbSet<webpages_Roles> webpages_Roles;


        private static IPracticeGDVPDao gdvpDao;

        public IDbSet<User> Users()
        {
            return users;
        }

        public static IPracticeGDVPDao GetInstance()
        {
            if (gdvpDao == null)
            {
                gdvpDao = Factory.Instance.Create<PracticeGDVPDao,IPracticeGDVPDao>();
            }
            return gdvpDao;
        }


        public IDbSet<BaseballCard> BaseballCards()
        {
            return baseBallCards;
        }

        public IDbSet<BaseballCardTransaction> BaseballCardTransactions()
        {
            return baseballCardTransactions;
        }

        public IDbSet<RuleSet> RuleSets()
        {
            return ruleSets;
        }

        public IDbSet<Store> Stores()
        {
            return stores;
        }

        public IDbSet<StoreInventory> StoreInventories()
        {
            return storeInventories;
        }

        public IDbSet<Transaction> Transactions()
        {
            return transactions;
        }

        public IDbSet<UserDiscount> UserDiscounts()
        {
            return userDiscounts;
        }


        public PracticeGDVPDao()
        {
            gdvp = Factory.Instance.Create<PracticeGDVPEntities>();
            users = gdvp.User;
            baseBallCards = gdvp.BaseballCards;
            baseballCardTransactions = gdvp.BaseballCardTransactions;
            transactions = gdvp.Transactions;
            userDiscounts = gdvp.UserDiscounts;
            ruleSets = gdvp.RuleSets;
            stores = gdvp.Stores;
            storeInventories = gdvp.StoreInventories;
            webpages_Roles = gdvp.webpages_Roles;

        }

        public int SaveChanges()
        {
            return gdvp.SaveChanges();
        }
        public void Dispose() { }


        IDbSet<webpages_Roles> IPracticeGDVPDao.webpages_Roles()
        {
            return webpages_Roles;
        }

        public DbEntityEntry Entry(object entity)
        {
            return gdvp.Entry(entity);
        }
    }
}