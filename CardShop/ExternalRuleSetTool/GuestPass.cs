using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Rules.Samples.ExternalRuleSetToolkit
{
    class GuestPass
    {
        public GuestPass(int id, decimal discount, string expiration)
        {
            this.CustomerRegionId = id;
            this.Discount = discount;
            this.Expiration = expiration;
        }

        private int CustomerRegionId;
        private decimal Discount;
        private string Expiration;

        public string ExpirationDate()
        {
            return this.Expiration;
        }
    }
}
