using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CardShop.Models;

namespace CardShop.Controllers
{
    public interface IAccountController
    {
        ActionResult Login(string returnUrl);
        ActionResult Login(LoginModel model, string returnUrl);
        ActionResult LogOff();
        ActionResult Register();
        ActionResult Register(RegisterModel model);
        ActionResult Manage(CardShop.Controllers.AccountController.ManageMessageId? message);
        ActionResult Manage(LocalPasswordModel model);
        ActionResult PasswordReset();
        ActionResult PasswordToken(PasswordReset resetModel);
        ActionResult PasswordUpdate(PasswordReset model);
    }
}
