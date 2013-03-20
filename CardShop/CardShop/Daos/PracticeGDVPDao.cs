using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Models;
using System.Data.Entity;

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


        private static PracticeGDVPDao gdvpDao;

        IDbSet<User> IPracticeGDVPDao.Users()
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
            users = new DbSetWrapper<User>(gdvp.Users);
            baseBallCards = new DbSetWrapper<BaseballCard>(gdvp.BaseballCards);
            baseballCardTransactions = new DbSetWrapper<BaseballCardTransaction>(gdvp.BaseballCardTransactions);
            transactions = new DbSetWrapper<Transaction>(gdvp.Transactions);
            userDiscounts = new DbSetWrapper<UserDiscount>(gdvp.UserDiscounts);
            ruleSets = new DbSetWrapper<RuleSet>(gdvp.RuleSets);
            stores = new DbSetWrapper<Store>(gdvp.Stores);
            storeInventories = new DbSetWrapper<StoreInventory>(gdvp.StoreInventories);
        }

        public int SaveChanges()
        {
            return gdvp.SaveChanges();
        }
        public void Dispose()
        {
            gdvp.Dispose();
        }
    }
}