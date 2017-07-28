using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using PhoneBookApi.Controllers;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using PhoneBookApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhoneBookApi
{

    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var provider = Membership.Provider;
            string name = provider.ApplicationName;
            var accCont = new AccountController();
            var email = context.Request.Headers["username"];
            var password = context.Request.Headers["password"];
            ClaimsIdentity identity = null;
            var manager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var appUser = manager.FindByName(email);
            var user = new ApplicationUser { UserName = email };
            var verified = manager.CheckPassword(appUser, password);
            if (verified)
            {
                identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, email));
                context.Validated();
            }
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var provider = Membership.Provider;
            string name = provider.ApplicationName;
            var accCont = new AccountController();
            var email = context.Request.Headers["username"];
            var password = context.Request.Headers["password"];
            /* var authen = FormsAuthentication.Authenticate(email, password);
             if (!authen) return;
             */
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var roles = Roles.GetRolesForUser(email);
            foreach (var role in roles)
            {
                switch (role.ToString().ToLower())
                {
                    case "admin":
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "StandardUser"));
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Guest"));
                        break;
                    case "StandardUser":
                        identity.AddClaim(new Claim(ClaimTypes.Role, "StandardUser"));
                        break;
                    default:
                        identity.AddClaim(new Claim(ClaimTypes.Role, "Guest"));
                        break;

                }
            }
            context.Validated(identity);
        }
    }
}
