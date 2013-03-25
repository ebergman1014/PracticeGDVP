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

        ActionResult Create();

        ActionResult Create(User user);

        ActionResult Edit(int id);

        ActionResult Edit(User user);

        ActionResult Delete(int id);

        ActionResult DeleteConfirmed(int id);

        ActionResult ActAsUser(int id);

        ActionResult StopActingAsUser();
    }
}
