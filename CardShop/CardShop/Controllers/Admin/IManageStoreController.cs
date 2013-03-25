using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CardShop.Models;

namespace CardShop.Controllers
{
    public interface IManageStoreController
    {
        ActionResult Index();
        ActionResult ManageStore(Store storeToChange);
        ActionResult ManageStore();
    }
}
