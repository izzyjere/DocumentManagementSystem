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

        // Asynchronously retrieves the current user.
        async Task GetUser()
        {
            currentUser = await _userService.GetCurrentUserAsync();
        }

        // Retrieves the user ID of the current user.
        public string GetUserId()
        {
            // Synchronously waits for GetUser() to complete and assigns the result to currentUser.
            Task.Run(GetUser).GetAwaiter().GetResult();
            return currentUser?.Id ?? string.Empty;
        }

        // Retrieves the user name of the current user.
        public string GetUserName()
        {
            // Synchronously waits for GetUser() to complete and assigns the result to currentUser.
            Task.Run(GetUser).GetAwaiter().GetResult();
            return currentUser?.UserName ?? string.Empty;
        }
    }
}
