using System;
using System.Web.Security;

namespace Buddy.WebApp.Providers
{
    public class AuthProvider :  RoleProvider
    {
       // private readonly SelfBillingRepository _repository;

        public AuthProvider()
        {
            //_repository = new SelfBillingRepository();
        }

        #region Implemented RoleProvider Methods

        public override bool IsUserInRole(string userName, string roleName)
        {
            /*
            User user = _repository.GetUser(userName);
            if (!_repository.UserExists(user)) return false;

            Role role;

            if (roleName.Contains(","))
            {
                role = _repository.GetRole(roleName);
                if (_repository.RoleExists(role))
                {
                    if (_repository.GetRole(user.role_id).code.ToLower() == role.code.ToLower())
                    {
                        return true;
                    }
                }
                return false;
            }
            role = _repository.GetRole(roleName);
            if (!_repository.RoleExists(role)) return false;
            return _repository.GetRole(user.role_id).code.ToLower() == role.code.ToLower();
            */
            return false;
        }

        public override string[] GetRolesForUser(string userName)
        {
            /*
            Role role = _repository.GetRoleForUser(userName);

            if (!_repository.RoleExists(role))
                return new[] {string.Empty};

            return new[] {role.code};
            */
            return new[] { "" };
        }

        #endregion

        #region Not Implemented RoleProvider Methods

        #region Properties

        /// <summary>
        ///   This property is not implemented.
        /// </summary>
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This method is not implemented.
        /// </summary>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This method is not implemented.
        /// </summary>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This method is not implemented.
        /// </summary>
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///   This function is not implemented.
        /// </summary>
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}