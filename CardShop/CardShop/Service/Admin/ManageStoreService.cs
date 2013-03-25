using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Models;
using CardShop.Daos;

namespace CardShop.Service
{
    public class ManageStoreService : IManageStoreService
    {
        public IPracticeGDVPDao context { get; set; }
        /// <summary>
        /// Edits the Discount to the Store
        /// </summary>
        /// <param name="changesToStore"></param>
        /// <returns></returns>
        public Store EditStore(Store ownedStore, Store changesToStore, out bool success)
        {
            success = false; 
            // grab store with the StoreId of the one wanting to change.
            if (changesToStore.DiscountRate > 0 && changesToStore.DiscountRate < 100)
            {
                // change the Discount rate
                ownedStore.DiscountRate = changesToStore.DiscountRate;
                context.SaveChanges();
                success = true;
            }
            // return store with new discountRate
            return ownedStore;
        }
        /// <summary>
        /// Find a Store which is owned by the ownerId
        /// </summary>
        /// <param name="ownerId"> Presumed store owner</param>
        /// <returns></returns>
        public Store OwnedStore(int ownerId)
        {
            // Create a bare store
            Store store = new Store();
            store.UserId = ownerId;
            // createList of stores
            var storeOwned = context.Stores().Where(s => s.UserId == ownerId).ToList();
            if (storeOwned.Count > 0)
            {
                store = storeOwned[0];
            }
            return store;
        }

        /// <summary>
        /// No-Args constructor
        /// sets singleton instance of PracticeGDVPDao
        /// </summary>
        public ManageStoreService()
        {
            context = PracticeGDVPDao.GetInstance();

        }
    }
}