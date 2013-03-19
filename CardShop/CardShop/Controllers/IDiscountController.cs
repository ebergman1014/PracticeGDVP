using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CardShop.Models;

namespace CardShop.Controllers
{
    /// <summary>
    /// Interface for DiscountController
    /// </summary>
    public interface IDiscountController
    {
        ActionResult Index();
        ActionResult IssueDiscount();
        UserDiscount IssueDiscount(UserDiscount coupon);
    }
}
