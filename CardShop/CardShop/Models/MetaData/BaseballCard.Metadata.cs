using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(BaseballCardMetaData))]
    public partial class BaseballCard
    {

    }


    public class BaseballCardMetaData
    {
        public int BaseballCardId { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public decimal Cost { get; set; }
    
        public virtual ICollection<BaseballCardTransaction> BaseballCardTransactions { get; set; }
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
