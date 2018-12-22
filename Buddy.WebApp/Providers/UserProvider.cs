using System;
using System.Configuration;
using System.Web.Security;
using Buddy.WebAPI.Client;
using Buddy.WebApp.Models;

namespace Buddy.WebApp.Providers
{
    public class UserProvider : MembershipProvider
    {
        private Client _client = null;

        public UserProvider()
        {
            //_repository = new SelfBillingRepository();
        }

        public int MinPasswordLength {
            get { return 5; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return MinPasswordLength; }
        }

        public string AccessToken { get; set; }

        public string LanguageCode { get; set; }

        public DateTime AccessTokenExpiresOn { get; set; }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                var apiKey = ConfigurationManager.AppSettings["api-key"];
                if (string.IsNullOrEmpty(apiKey))
                    throw new Exception("No Api Key configured");

                var apiUrl = ConfigurationManager.AppSettings["api-url"];
                if (string.IsNullOrEmpty(apiUrl))
                    throw new Exception("No Api URL configured");

                _client = new Client(apiUrl);

                if (_client != null)
                {
                    var loginResult = _client.Login(username, password, apiKey, LanguageCode);

                    if (loginResult != null)
                    {
                        if (!string.IsNullOrEmpty(loginResult.AccessToken))
                        {
                            AccessToken = loginResult.AccessToken;
                            AccessTokenExpiresOn = DateTime.Now.AddMinutes(Convert.ToInt32(loginResult.ExpiresIn));

                            return true;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(loginResult.Error))
                                throw new Exception(loginResult.ErrorDescription);
                            else
                                return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public void CreateUser(string username, string fullName, string password, string email, string roleName)
        {
            //_repository.CreateUser(username, fullName, password, email, roleName);
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            /*
            if (!ValidateUser(username, oldPassword) || string.IsNullOrEmpty(newPassword.Trim()))
                return false;

            User user = _repository.GetUser(username);
            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword.Trim(), "md5");

            user.password = hash;
            _repository.Save();

            return true;
            */
            return true;
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                             string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override MembershipUser CreateUser(string username, string password, string email,
                                                  string passwordQuestion, string passwordAnswer, bool isApproved,
                                                  object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
                                                                  out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
                                                                 out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var apiKey = ConfigurationManager.AppSettings["api-key"];
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("No Api Key configured");

            var apiUrl = ConfigurationManager.AppSettings["api-url"];
            if (string.IsNullOrEmpty(apiUrl))
                throw new Exception("No Api URL configured");

            _client = new Client(apiUrl);

            var users = new MembershipUserCollection();

            var allUsers = _client.Get(apiUrl + "api/user");

            totalRecords = 1;
            return users;
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var apiKey = ConfigurationManager.AppSettings["api-key"];
            if (string.IsNullOrEmpty(apiKey))
                throw new Exception("No Api Key configured");

            var apiUrl = ConfigurationManager.AppSettings["api-url"];
            if (string.IsNullOrEmpty(apiUrl))
                throw new Exception("No Api URL configured");

            _client = new Client(apiUrl);

            var totalRecords = 0;
            var allUsers = GetAllUsers(0, 0, out totalRecords);

            var newU = allUsers["jordan"];
            //User.Identity.Name
            return newU;
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This method is not implemented.
        /// </summary>
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

#region Properties

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

#endregion
    }
}