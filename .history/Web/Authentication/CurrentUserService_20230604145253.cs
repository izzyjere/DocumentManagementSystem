using RTSADocs.Data.Services;

using SimpleAuthentication;

namespace RTSADocs.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        readonly IUserService _userService;
        UserProxy? currentUser;
        public CurrentUserService(IUserService userService)
        {
            _userService = userService;
        
        }
        async Task GetUser()
        {
            currentUser = await _userService.GetCurrentUserAsync();
        }
        public string GetUserId()
        {
            Task.Run(GetUser).GetAwaiter().GetResult();
            return currentUser?.Id??string.Empty;
        }

        public string GetUserName()
        {
            Task.Run(GetUser).GetAwaiter().GetResult();
            return currentUser?.UserName ?? string.Empty;
        }
    }
}
