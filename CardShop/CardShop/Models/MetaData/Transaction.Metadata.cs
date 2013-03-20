using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(TransactionMetaData))]
    public partial class Transaction
    {

    }

    public class TransactionMetaData
    {
    
        public int TransactionId { get; set; }
        public int StoreId { get; set; }
        public decimal TotalCost { get; set; }
        public System.DateTime Date { get; set; }
        public int UserId { get; set; }
        public decimal DiscountRate { get; set; }
    
        public virtual ICollection<BaseballCardTransaction> BaseballCardTransactions { get; set; }
        public virtual Store Store { get; set; }
        public virtual User UserTable { get; set; }
    }
}
