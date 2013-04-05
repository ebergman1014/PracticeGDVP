using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Rules.Samples.ExternalRuleSetToolkit
{
    public class Store
    {
        public Store(int StoreId, int UserId, string Name, decimal DiscountRate)
        {
            this.StoreId = StoreId;
            this.UserId = UserId;
            this.Name = Name;
            this.DiscountRate = DiscountRate;
        }

        public int StoreId;
        public int UserId;
        public string Name;
        public decimal DiscountRate;
    }
}
