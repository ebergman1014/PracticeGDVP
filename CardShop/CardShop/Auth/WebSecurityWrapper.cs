using CardShop.Daos;
using CardShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using WebMatrix.WebData;

namespace CardShop.Auth
{
    public class WebSecurityWrapper : IWebSecurity
    {

        private static IWebSecurity _Instance;

        public static IWebSecurity Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new WebSecurityWrapper();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        public bool Login(string userName, string password, bool persistCookie = false)
        {
            bool success = WebSecurity.Login(userName, password, persistCookie);
            if (success)
            {
                UserAuth.Current.Login(HttpContext.Current.User);
            }
            return success;
        }

        public void Logout()
        {
            UserAuth.Current.Logout();
            WebSecurity.Logout();
        }

        public string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues);
        }

        public int GetUserId(string userName)
        {
            return WebSecurity.GetUserId(userName);
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public string CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateAccount(userName, password, requireConfirmationToken);
        }

        public bool ResetPassword(string passwordResetToken, string newPassword)
        {
            return WebSecurity.ResetPassword(passwordResetToken, newPassword);
        }

        public string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow = 1440)
        {
            return WebSecurity.GeneratePasswordResetToken(userName, tokenExpirationInMinutesFromNow);
        }

        public IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }

        public int CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }

        public string CurrentUserName
        {
            get { return WebSecurity.CurrentUserName; }
        }
    }
}
