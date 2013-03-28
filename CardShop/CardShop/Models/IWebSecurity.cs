using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace CardShop.Models
{
    public interface IWebSecurity
    {
        bool Login(string userName, string password, bool persistCookie = false);
        void Logout();
        string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false);
        int GetUserId(string userName);
        bool ChangePassword(string userName, string currentPassword, string newPassword);
        string CreateAccount(string userName, string password, bool requireConfirmationToken = false);
        bool ResetPassword(string passwordResetToken, string newPassword);
        string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow = 1440);

        IPrincipal CurrentUser { get; }
    }
}

