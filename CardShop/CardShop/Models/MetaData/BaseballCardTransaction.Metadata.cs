using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(BaseballCardTransactionMetaData))]
    public partial class BaseballCardTransaction
    {

    }    
    public class BaseballCardTransactionMetaData
    {
        public int BaseballCardTransactionId { get; set; }
        public int BaseballCardId { get; set; }
        public int TransactionId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
    
        public virtual BaseballCard BaseballCard { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
