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


        private static PracticeGDVPDao gdvpDao;

        public IDbSet<User> Users()
        {
            return users;
        }

        public static PracticeGDVPDao GetInstance()
        {
            if (gdvpDao == null)
            {
                gdvpDao = new PracticeGDVPDao();
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


        private PracticeGDVPDao()
        {
            gdvp = new PracticeGDVPEntities();
            users = Factory.Instance.Create<DbSetWrapper<User>>(gdvp.User);
            baseBallCards = Factory.Instance.Create<DbSetWrapper<BaseballCard>>(gdvp.BaseballCards);
            baseballCardTransactions = Factory.Instance.Create<DbSetWrapper<BaseballCardTransaction>>(gdvp.BaseballCardTransactions);
            transactions = Factory.Instance.Create<DbSetWrapper<Transaction>>(gdvp.Transactions);
            userDiscounts = Factory.Instance.Create<DbSetWrapper<UserDiscount>>(gdvp.UserDiscounts);
            ruleSets = Factory.Instance.Create<DbSetWrapper<RuleSet>>(gdvp.RuleSets);
            stores = Factory.Instance.Create<DbSetWrapper<Store>>(gdvp.Stores);
            storeInventories = Factory.Instance.Create<DbSetWrapper<StoreInventory>>(gdvp.StoreInventories);
            webpages_Roles = Factory.Instance.Create<DbSetWrapper<webpages_Roles>>(gdvp.webpages_Roles);

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