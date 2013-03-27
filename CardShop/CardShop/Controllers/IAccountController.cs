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
        ActionResult Disassociate(string provider, string providerUserId);
        ActionResult Manage(CardShop.Controllers.AccountController.ManageMessageId? message);
        ActionResult Manage(LocalPasswordModel model);
        ActionResult PasswordReset();
        ActionResult PasswordToken(PasswordReset resetModel);
        ActionResult PasswordUpdate(PasswordReset model);
        ActionResult ExternalLogin(string provider, string returnUrl);
        ActionResult ExternalLoginCallback(string returnUrl);
        ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl);
        ActionResult ExternalLoginFailure();
        ActionResult ExternalLoginsList(string returnUrl);
        ActionResult RemoveExternalLogins();
    }
}
