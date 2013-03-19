using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CardShop.Utilities
{
    public class CouponUtility
    {
        public string GenerateCoupon() {
            StringBuilder discountCode = new StringBuilder();
            Random random = new Random();
            for (int index = 0; index < 5; index++)
            {
                discountCode.Append(Convert.ToChar(random.Next(65, 90)));
            }
            return discountCode.ToString();
        }
    }
}