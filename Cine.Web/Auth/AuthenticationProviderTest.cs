using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Cine.WEB.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(3000);
            var anonimous = new ClaimsIdentity();
            var oapUser = new ClaimsIdentity(new List<Claim>
        {
            new Claim("FirstName", "Luis"),
            new Claim("LastName", "Orozco"),
            new Claim(ClaimTypes.Name, "oap@yopmail.com"),
            new Claim(ClaimTypes.Role, "Admin")
        },
        authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(oapUser)));
        }
    }
}
