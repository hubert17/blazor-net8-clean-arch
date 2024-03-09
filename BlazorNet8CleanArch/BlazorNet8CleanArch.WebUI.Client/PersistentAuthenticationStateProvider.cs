using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorNet8CleanArch.WebUI.Client
{
    public class AddHeadersDelegatingHandler : DelegatingHandler
    {
        public AddHeadersDelegatingHandler() : base(new HttpClientHandler())
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Bearer", StorageConstants.Local.JWTToken);  // Add whatever headers you want here

            return base.SendAsync(request, cancellationToken);
        }
    }

    public record UserInfo(string Name = null!, string[] Roles = null!);

    public class PersistentAuthenticationStateProvider : AuthenticationStateProvider
    {
        readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        readonly ILocalStorageService _localStorage;

        private static readonly Task<AuthenticationState> defaultUnauthenticatedTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        private readonly Task<AuthenticationState> authenticationStateTask = defaultUnauthenticatedTask;

        public PersistentAuthenticationStateProvider(PersistentComponentState state, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;

            if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
            {
                return;
            }

            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, userInfo.Name),
                    new(ClaimTypes.Name, userInfo.Name),
                    new(ClaimTypes.Email, userInfo.Name),
                };
            foreach (var role in userInfo.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            authenticationStateTask = Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: nameof(PersistentAuthenticationStateProvider)))));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var jwtToken = await GetToken();
                if (string.IsNullOrEmpty(jwtToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));
                else
                {
                    var getUserClaims = DecryptToken(jwtToken);
                    if (getUserClaims == null)
                        return await Task.FromResult(new AuthenticationState(anonymous));
                    else
                    {
                        var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                        return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                    }
                }
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public ClaimsPrincipal SetClaimPrincipal(UserInfo customUserClaims)
        {
            if (string.IsNullOrEmpty(customUserClaims.Name))
                return new ClaimsPrincipal();
            else
            {
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, customUserClaims.Name),
                    new(ClaimTypes.Name, customUserClaims.Name),
                    new(ClaimTypes.Email, customUserClaims.Name),
                };
                foreach (var role in customUserClaims.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: nameof(PersistentAuthenticationStateProvider)));
            }

        }

        private async void SaveToken(string jwtToken)
        {
            StorageConstants.Local.JWTToken = jwtToken;
            try
            {
                await _localStorage.SetItemAsync("jwtToken", jwtToken);
            }
            catch { }
        }

        private async Task<string?> GetToken()
        {
            try
            {
                return await _localStorage.GetItemAsStringAsync("jwtToken");
            }
            catch 
            {
                return StorageConstants.Local.JWTToken;
            }
        }

        public void UpdateAuthenticationState(string jwtToken = "")
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                SaveToken(jwtToken);

                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                SaveToken(null!);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private UserInfo DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                return new UserInfo();
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwtToken);

                var name = token.Claims.FirstOrDefault(x => x.Type == "name");
                var roles = token.Claims.Where(x => x.Type == "role").Select(s => s.Value).ToArray();

                return new UserInfo(name!.Value, roles);
            }
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("jwtToken");

            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
