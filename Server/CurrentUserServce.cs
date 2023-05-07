using RTSADocs.Data.Services;

using System.Security.Claims;

namespace RTSADocs.Server
{
    internal class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
              this.contextAccessor = contextAccessor;
        }
        public string GetUserId()
        {
            var context = contextAccessor.HttpContext;
            if (context is not null)
            {
                var user = context.User;
                return user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

            }
            return string.Empty;
        }

        public string GetUserName()
        {
            var context = contextAccessor.HttpContext;            
            if (context is not null)
            {
                var user = context.User;                 
                return user?.Identity?.Name ?? "Anonymus";     

            }
            return "SYSTEM";
        }
    }
}
