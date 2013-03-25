using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Daos
{
    /// <summary>
    /// Wrapper interface for PracticeGDVPEntities
    /// </summary>
    public interface IPracticeGDVPDao : IDisposable
    {
        /// <summary>
        /// DbSet for User
        /// </summary>
        /// <returns></returns>
        IDbSet<User> Users();
        /// <summary>
        /// DbSet for BaseballCard
        /// </summary>
        /// <returns></returns>
        IDbSet<BaseballCard> BaseballCards();
        /// <summary>
        /// DbSet for BaseballCardTransaction
        /// </summary>
        /// <returns></returns>
        IDbSet<BaseballCardTransaction> BaseballCardTransactions();
        /// <summary>
        /// DbSet for RuleSet
        /// </summary>
        /// <returns></returns>
        IDbSet<RuleSet> RuleSets();
        /// <summary>
        /// DbSet for Store
        /// </summary>
        /// <returns></returns>
        IDbSet<Store> Stores();
        /// <summary>
        /// DbSet for StoreInvetory
        /// </summary>
        /// <returns></returns>
        IDbSet<StoreInventory> StoreInventories();
        /// <summary>
        /// DbSet for Transaction
        /// </summary>
        /// <returns></returns>
        IDbSet<Transaction> Transactions();
        /// <summary>
        /// DbSet UserDiscount
        /// </summary>
        /// <returns></returns>
        IDbSet<UserDiscount> UserDiscounts();


        IDbSet<webpages_Roles> webpages_Roles();
        int SaveChanges();

        DbEntityEntry Entry(object entity);
    }
}
