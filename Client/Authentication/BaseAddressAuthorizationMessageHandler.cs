using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using System.Net;
using System.Net.Http.Headers;

namespace RTSADocs.Client.Authentication
{
    public class CustomAuthorizationMessageHandler : DelegatingHandler
    {

        private readonly IAccessTokenProvider _provider;

        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider)
        {
            _provider = provider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tokenResult = await _provider.RequestAccessToken();
            if (tokenResult.Status == AccessTokenResultStatus.Success)
            {
                tokenResult.TryGetToken(out var token);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            else
            {
                request.Headers.Authorization = null;
            }
            return await base.SendAsync(request, cancellationToken);
        }


    }

}
