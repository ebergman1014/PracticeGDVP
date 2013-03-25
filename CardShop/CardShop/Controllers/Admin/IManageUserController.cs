using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CardShop.Models;

namespace CardShop.Controllers.Admin
{
    interface IManageUserController
    {


        ActionResult Index();

        ActionResult Details(int id);

        [HttpPost]
        ActionResult Create();

        ActionResult Create(User user);

        ActionResult Edit(int id);

        [HttpPost]
        ActionResult Edit(User user);

        ActionResult Delete(int id);

        [HttpPost, ActionName("Delete")]
        ActionResult DeleteConfirmed(int id);

    }
}
