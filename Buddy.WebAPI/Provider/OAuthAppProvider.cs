using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Buddy.CodeFirst;
using Buddy.CodeFirst.Models.User;
using Buddy.WebAPI.Dtos.User;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Buddy.WebAPI.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        private BuddyContext _context;

        public OAuthAppProvider()
        {
            _context = new BuddyContext();
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                var clientId = context.ClientId;
                
                var user = _context.Users.SingleOrDefault(c => c.UserLogin.UserName == username);

                var isValid = false;
                if (user != null)
                {
                    var hashedPassword = user.UserLogin.PasswordHash;
                    isValid = BCryptHelper.CheckPassword(password, hashedPassword);
                }

                if (isValid)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.UserLogin.UserName),
                        new Claim("UserID", user.Id.ToString())
                    };

                    var oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }
         
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}