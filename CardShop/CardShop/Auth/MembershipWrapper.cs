using CardShop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace CardShop.Auth
{
    public class MembershipWrapper : IMembership
    {

        private static MembershipWrapper wrapper;
        public MembershipWrapper(){
        }
        public static IMembership getInstance(){
            if (wrapper == null)
            {
                wrapper = Factory.Instance.Create<MembershipWrapper>();

            }
            return wrapper;
        }

        public string ApplicationName
        {
            get
            {
                return Membership.ApplicationName;
            }
            set
            {
                Membership.ApplicationName = value;
            }
        }

        public bool EnablePasswordReset
        {
            get { return Membership.EnablePasswordReset; }
        }

        public bool EnablePasswordRetrieval
        {
            get { return Membership.EnablePasswordRetrieval; }
        }

        public string HashAlgorithmType
        {
            get { return Membership.HashAlgorithmType; }
        }

        public int MaxInvalidPasswordAttempts
        {
            get { return Membership.MaxInvalidPasswordAttempts; }
        }

        public int MinRequiredNonAlphanumericCharacters
        {
            get { return Membership.MinRequiredNonAlphanumericCharacters; }
        }

        public int MinRequiredPasswordLength
        {
            get { return Membership.MinRequiredPasswordLength; }
        }

        public int PasswordAttemptWindow
        {
            get { return Membership.PasswordAttemptWindow; }
        }

        public string PasswordStrengthRegularExpression
        {
            get { return Membership.PasswordStrengthRegularExpression; }
        }

        public MembershipProvider Provider
        {
            get { return Membership.Provider; }
        }

        public MembershipProviderCollection Providers
        {
            get { return Membership.Providers; }
        }

        public bool RequiresQuestionAndAnswer
        {
            get { return Membership.RequiresQuestionAndAnswer; }
        }

        public int UserIsOnlineTimeWindow
        {
            get { return Membership.UserIsOnlineTimeWindow; }
        }

        public event MembershipValidatePasswordEventHandler ValidatingPassword;

        public MembershipUser CreateUser(string username, string password)
        {
            return Membership. CreateUser(username, password);
        }

        public MembershipUser CreateUser(string username, string password, string email)
        {
            return Membership.CreateUser(username, password, email);
        }

        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status)
        {
            return Membership.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, out status);
        }

        public MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return Membership.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public bool DeleteUser(string username)
        {
            return Membership.DeleteUser(username);
        }

        public bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return Membership.DeleteUser(username, deleteAllRelatedData);
        }

        public MembershipUserCollection FindUsersByEmail(string emailToMatch)
        {
            return Membership.FindUsersByEmail(emailToMatch);
        }

        public MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return Membership.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        public MembershipUserCollection FindUsersByName(string usernameToMatch)
        {
            return Membership.FindUsersByName(usernameToMatch);
        }

        public MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return Membership.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            return Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
        }

        public MembershipUserCollection GetAllUsers()
        {
            return Membership.GetAllUsers();
        }

        public MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return Membership.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public int GetNumberOfUsersOnline()
        {
            return Membership.GetNumberOfUsersOnline();
        }

        public MembershipUser GetUser()
        {
            return Membership.GetUser();
        }

        public MembershipUser GetUser(bool userIsOnline)
        {
            return Membership.GetUser(userIsOnline);
        }

        public MembershipUser GetUser(object providerUserKey)
        {
            return Membership.GetUser(providerUserKey);
        }

        public MembershipUser GetUser(string username)
        {
            return Membership.GetUser(username);
        }

        public MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return Membership.GetUser(providerUserKey, userIsOnline);
        }

        public MembershipUser GetUser(string username, bool userIsOnline)
        {
            return Membership.GetUser(username, userIsOnline);
        }

        public string GetUserNameByEmail(string emailToMatch)
        {
            return Membership.GetUserNameByEmail(emailToMatch);
        }

        public void UpdateUser(MembershipUser user)
        {
            Membership.UpdateUser(user);
        }

        public bool ValidateUser(string username, string password)
        {
            return Membership.ValidateUser(username, password);
        }

        public int GetUserId()
        {
            return Convert.ToInt32(GetUser().ProviderUserKey);
        }
    }

}