using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Buddy.WebApp.Security
{
    public static class Extensions
    {
        private static readonly List<UserExtension> Items = new List<UserExtension>();

        private struct UserExtension
        {            
            public string Username;
            public List<SecurityRight> SecurityRights;
            public DateTime? AccessTokenExpiresOn;
            public string AccessToken;
        }
        
        public class SecurityRight
        {
            public int id { get; set; }
        }

        public static void LogOut(this IPrincipal user)
        {
            if (user == null) return;
            var userExtension = new UserExtension();

            if (Items != null)
                userExtension = Items.FirstOrDefault(e => String.Equals(e.Username, user.Identity.Name, StringComparison.CurrentCultureIgnoreCase));
            if (userExtension.Username != null) if (Items != null) Items.Remove(userExtension);
        }

        private static UserExtension LoadData(this IPrincipal user, bool refresh = false)
        {
            var userExtension = new UserExtension();

            if (user == null || user.Identity.Name == "") return userExtension;
            
            return userExtension;
        }

        public static List<SecurityRight> SecurityRights(this IPrincipal user)
        {
            var userExtension = LoadData(user);
            return userExtension.SecurityRights;
        }

        public static void Refresh(this IPrincipal user)
        {
            if (user == null) return;
            LoadData(user, true);
        }

        public static string AccessToken(this IPrincipal user, string accessToken)
        {
            return "";
        }

        public static DateTime? AccessTokenExpiresOn(this IPrincipal user, DateTime? input)
        {
            return null;
        }
    }
}