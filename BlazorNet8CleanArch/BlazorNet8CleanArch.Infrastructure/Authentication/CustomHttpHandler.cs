using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BlazorNet8CleanArch.Infrastructure.Authentication
{
    public class CustomHttpHandler : DelegatingHandler
    {
        readonly ILocalStorageService _localStorage;
        readonly AuthenticationStateProvider _authStateProvider;

        public CustomHttpHandler(ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : base(new HttpClientHandler())
        {
            _localStorage = localStorageService;
            _authStateProvider = authenticationStateProvider;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsolutePath.ToLower().Contains("login") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("register") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("tokenrefresh"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var jwtToken = await _localStorage.GetItemAsStringAsync(StorageConstants.Local.JWTTokenStorageKeyName);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            var originalResponse = await base.SendAsync(request, cancellationToken);
            if (originalResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await InvokeRefreshCall(request, originalResponse, jwtToken!, cancellationToken);
            }

            return originalResponse;
        }

        private async Task<HttpResponseMessage> InvokeRefreshCall(HttpRequestMessage originalRequest,
            HttpResponseMessage originalResponse,
            string expiredJwtToken,
            CancellationToken cancellationToken)
        {
            var refreshToken = await _localStorage.GetItemAsStringAsync("refreshToken");
            var jsonPayload = JsonSerializer.Serialize(new { token = expiredJwtToken, refreshToken });
            var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            using (var refreshTokenClient = new HttpClient())
            {
                var refreshTokenResponse = await refreshTokenClient.PostAsync($"{StorageConstants.Local.HttpClientAuthBaseAddress}/TOKENREFRESH?token={expiredJwtToken}&refreshToken={refreshToken}", requestContent);
                if (refreshTokenResponse.StatusCode == HttpStatusCode.OK)
                {
                    var regeneratedTokenResponse = await refreshTokenResponse.Content.ReadFromJsonAsync<JWTTokenResponse>();
                    await _localStorage.SetItemAsStringAsync(StorageConstants.Local.JWTTokenStorageKeyName, regeneratedTokenResponse!.Token);
                    await _localStorage.SetItemAsStringAsync("refreshToken", regeneratedTokenResponse.RefreshToken);
                    (_authStateProvider as PersistentAuthenticationStateProvider)!.UpdateAuthenticationState(regeneratedTokenResponse.Token);

                    originalRequest.Headers.Remove("Authorization");
                    originalRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", regeneratedTokenResponse!.Token);

                    return await base.SendAsync(originalRequest, cancellationToken);
                }
            }
           
            return originalResponse;
        }

        public record JWTTokenResponse(string Username = null!, string Token = null!, string RefreshToken = null!);
    }
}
