using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(StoreMetaData))]
    public partial class Store
    {

    }

    public class StoreMetaData
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal DiscountRate { get; set; }
    
        public virtual User UserTable { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
