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
            GetUser();
        }
        async void GetUser()
        {
            currentUser = await _userService.GetCurrentUserAsync();
        }
        public string GetUserId()
        {
            return currentUser?.Id??string.Empty;
        }

        public string GetUserName()
        {
            return currentUser?.UserName ?? string.Empty;
        }
    }
}
