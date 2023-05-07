using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using RTSADocs.Shared.DTOs;

using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
namespace RTSADocs.Client.Authentication
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService localStorageService;

        public ServerAuthenticationStateProvider(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
        {
            _httpClient = httpClientFactory.CreateClient("ServerAPI");
            this.localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var result = await _httpClient.PostAsync("token", null);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<TokenResponse>();                 
                var identity = new ClaimsIdentity(GetAllClaims(response.Token), "jwt");
                var user = new ClaimsPrincipal(identity);
                await SaveToken(response.Token);
                return new AuthenticationState(user);
            }
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        async Task SaveToken(string token)
        {
            var accessToken = new AccessToken() { Value = token };
            await localStorageService.SetItemAsync("token", accessToken);
        }
        static IEnumerable<Claim> GetAllClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);            
            return securityToken.Claims.ToList();
        }
    }

}
