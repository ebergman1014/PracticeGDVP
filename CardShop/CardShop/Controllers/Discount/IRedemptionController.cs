using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CardShop.Controllers
{
    /// <summary>
    /// Interface for RedemptionController
    /// </summary>
    interface IRedemptionController
    {
        ActionResult Index();
        ActionResult Redeem();
        ActionResult RedeemDiscount(int userId, String couponCode);
    }
}
