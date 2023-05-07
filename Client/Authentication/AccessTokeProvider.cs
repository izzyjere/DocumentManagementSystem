using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace RTSADocs.Client.Authentication
{
    public class AccessTokeProvider : IAccessTokenProvider
    {
        private readonly ILocalStorageService localStorage;
        public AccessTokeProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }
        public async ValueTask<AccessTokenResult> RequestAccessToken()
        {
            var token = await localStorage.GetItemAsync<AccessToken>("token");
            if(token is not null)
            {
                return new AccessTokenResult(AccessTokenResultStatus.Success,token,string.Empty,null);
            }
            return new AccessTokenResult(AccessTokenResultStatus.RequiresRedirect,null,"token",null);
        }

        public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            return RequestAccessToken();
        }
    }
}
