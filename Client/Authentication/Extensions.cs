using System.Security.Claims;

namespace RTSADocs.Client.Authentication
{
    public static class Extensions
    {
        public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) => principal?.FindFirst(claimType)?.Value;
        public static string? GetUserName(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }
}
