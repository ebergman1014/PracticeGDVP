using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Models;
using CardShop.Daos;

namespace CardShop.Service
{
    public class AdminService : IAdminService
    {
        public IPracticeGDVPDao context { get; set; }
        /// <summary>
        /// Edits the Discount to the Store
        /// </summary>
        /// <param name="changesToStore"></param>
        /// <returns></returns>
        public Store EditDiscount(Store changesToStore)
        {
            var store = new Store();
            // grab store with the StoreId of the one wanting to change.
            if (changesToStore.DiscountRate > 0 && changesToStore.DiscountRate < 100)
            {
                store = context.Stores().Find(changesToStore.StoreId);
                // change the Discount rate
                store.DiscountRate = changesToStore.DiscountRate;
                context.SaveChanges();
            }
            // return store with new discountRate
            return store;
        }

        public Store OwnedStore(int ownerId)
        {
            return null;
        }

        /// <summary>
        /// No-Args constructor
        /// sets singleton instance of PracticeGDVPDao
        /// </summary>
        public AdminService()
        {
            context = PracticeGDVPDao.GetInstance();
        }
    }
}