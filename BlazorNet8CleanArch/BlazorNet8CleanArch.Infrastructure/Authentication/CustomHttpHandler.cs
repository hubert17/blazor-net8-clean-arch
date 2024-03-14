using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Constants;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Infrastructure.Authentication
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationStateProvider _authStateProvider;
        public CustomHttpHandler(ILocalStorageService localStorageService,  IHttpClientFactory httpClientFactory, AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorage = localStorageService;
            _httpClientFactory = httpClientFactory;
            _authStateProvider = authenticationStateProvider;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsolutePath.ToLower().Contains("login") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("register") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("TOKENREFRESH"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var jwtToken = await _localStorage.GetItemAsStringAsync(StorageConstants.Local.JWTTokenStorageKeyName);

            if (!string.IsNullOrEmpty(jwtToken))
            {
                //request.Headers.Add("Authorization", $"bearer {jwtToken}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);
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
            var jwtToken = await _localStorage.GetItemAsStringAsync(StorageConstants.Local.JWTTokenStorageKeyName);
            var refreshToken = await _localStorage.GetItemAsStringAsync("refreshToken");
            
            var httpClient = _httpClientFactory.CreateClient("api45gabs");
            var jsonPayload = JsonSerializer.Serialize(new { jwtToken, refreshToken });
            var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var refreshTokenresponse = await httpClient.PostAsync($"https://api45gabs.azurewebsites.net/TOKENREFRESH?token={jwtToken}&refreshToken={refreshToken}", requestContent);

            if (refreshTokenresponse.StatusCode == HttpStatusCode.OK)
            {
                var regeneratedTokenResponse = await refreshTokenresponse.Content.ReadFromJsonAsync<JWTTokenResponse>();
                await _localStorage.SetItemAsStringAsync(StorageConstants.Local.JWTTokenStorageKeyName, regeneratedTokenResponse!.Token);
                await _localStorage.SetItemAsStringAsync("refreshToken", regeneratedTokenResponse.RefreshToken);
                (_authStateProvider as PersistentAuthenticationStateProvider)!.UpdateAuthenticationState(regeneratedTokenResponse.Token);

                originalRequest.Headers.Remove("Authorization");
                //originalRequest.Headers.Add("Authorization", $"bearer {regeneratedTokenResponse.Token}");
                originalRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", regeneratedTokenResponse!.Token);

                return await base.SendAsync(originalRequest, cancellationToken);
            }
            return originalResponse;
        }

        public record JWTTokenResponse(string Username = null!, string Token = null!, string RefreshToken = null!);
    }
}
