using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RTSADocs.Client.Authentication
{
    public class WindowsAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, Environment.UserName),
        }, "Windows");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }

}
