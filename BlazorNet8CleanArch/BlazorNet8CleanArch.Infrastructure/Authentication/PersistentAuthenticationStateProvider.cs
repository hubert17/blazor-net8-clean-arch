﻿using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorNet8CleanArch.Infrastructure.Authentication
{
    public class PersistentAuthenticationStateProvider : AuthenticationStateProvider
    {
        readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        static readonly Task<AuthenticationState> defaultUnauthenticatedTask = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        readonly Task<AuthenticationState> authenticationStateTask = defaultUnauthenticatedTask;

        readonly ILocalStorageService _localStorage;

        public PersistentAuthenticationStateProvider(PersistentComponentState state, ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;

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
                var jwtToken = await _localStorage.GetItemAsStringAsync(StorageConstants.Local.JWTTokenStorageKeyName);
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
        public void UpdateAuthenticationState(string jwtToken = "")
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
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
            await _localStorage.RemoveItemAsync(StorageConstants.Local.JWTTokenStorageKeyName);
            await _localStorage.RemoveItemAsync("refreshToken");

            var authState = Task.FromResult(new AuthenticationState(anonymous));

            NotifyAuthenticationStateChanged(authState);
        }
    }

    public record UserInfo(string Name = null!, string[] Roles = null!); 
}
