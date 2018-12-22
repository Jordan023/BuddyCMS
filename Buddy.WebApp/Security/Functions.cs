using System.Linq;

namespace Buddy.WebApp.Security
{
    public static class Functions
    {
        public static bool IsUserInRole(System.Security.Principal.IPrincipal user, string roles)
        {
            var rolesList = roles.Split(',').ToList();
            foreach (var r in rolesList)
            {
                if (user.IsInRole(r)) return true;
            }
            return false;
        }        

        public static bool IsUserInRole(System.Security.Principal.IPrincipal user, string controller, string action)
        {
            if (user.SecurityRights() != null)
            {
                controller = controller.ToLower();
                action = action.ToLower();

                return true;
                //return user.SecurityRights().Any(s => s.controller.ToLower() == controller && s.action.ToLower() == action);
            }
            else
            {
                return true;

                //var repository = new SelfBillingRepository();
                //return repository.IsUserValid(user.Identity.Name, controller, action);
            }
        }
    }
}