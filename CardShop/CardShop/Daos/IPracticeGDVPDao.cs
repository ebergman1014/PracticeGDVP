using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        IDbSet<User> Users();
        IDbSet<BaseballCard> BaseballCards();
        IDbSet<BaseballCardTransaction> BaseballCardTransactions();
        IDbSet<RuleSet> RuleSets();
        IDbSet<Store> Stores();
        IDbSet<StoreInventory> StoreInventories();
        IDbSet<Transaction> Transactions();
        IDbSet<UserDiscount> UserDiscounts();
    }
}
