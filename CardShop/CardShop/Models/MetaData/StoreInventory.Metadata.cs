using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(StoreInventoryMetaData))]
    public partial class StoreInventory
    {

    }

    public class StoreInventoryMetaData
    {
        public int StoreInventoryId { get; set; }
        public int BaseballCardId { get; set; }
        public int StoreId { get; set; }
        public int Quantity { get; set; }
    
        public virtual BaseballCard BaseballCard { get; set; }
        public virtual Store Store { get; set; }
    }
}
