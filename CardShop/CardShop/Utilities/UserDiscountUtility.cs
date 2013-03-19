using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CardShop.Utilities
{
    /// <summary>
    /// User Discount related utilities
    /// </summary>
    public class UserDiscountUtility : IUserDiscountUtility
    {
        // Instead of using ASCII magic numbers
        private readonly static int A = 65;
        private readonly static int Z = 90;

        /// <summary>
        /// Generates a code for a consumer to use
        /// </summary>
        /// <returns> A CAP-ALPHA code of 5 digits</returns>
        public string GenerateCoupon()
        {
            // Stringbuilder for easy appending
            StringBuilder discountCode = new StringBuilder();
            Random random = new Random();
            for (int index = 0; index < 5; index++)
            {
                // A and Z are ASCII integers
                discountCode.Append(Convert.ToChar(random.Next(A, Z)));
            }
            return discountCode.ToString();
        }
    }
}