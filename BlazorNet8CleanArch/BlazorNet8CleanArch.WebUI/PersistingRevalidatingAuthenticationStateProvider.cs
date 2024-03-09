﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace BlazorNet8CleanArch.WebUI
{
    // This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
    // every 30 minutes an interactive circuit is connected. It also uses PersistentComponentState to flow the
    // authentication state to the client which is then fixed for the lifetime of the WebAssembly application.

    public class PersistingRevalidatingAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly PersistentComponentState _state;
        private readonly IdentityOptions _options;

        private readonly PersistingComponentStateSubscription _subscription;

        private Task<AuthenticationState>? _authenticationStateTask;

        public PersistingRevalidatingAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            PersistentComponentState state,
            IOptions<IdentityOptions> options)
            : base(loggerFactory)
        {
            _scopeFactory = scopeFactory;
            _state = state;
            _options = options.Value;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            _subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override async Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            // Get the user manager from a new scope to ensure it fetches fresh data
            await using var scope = _scopeFactory.CreateAsyncScope();
            return ValidateSecurityStampAsync(authenticationState.User);
        }

        private bool ValidateSecurityStampAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity?.IsAuthenticated is false)
            {
                return false;
            }

            return true;
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
        {
            _authenticationStateTask = authenticationStateTask;
        }

        private async Task OnPersistingAsync()
        {
            if (_authenticationStateTask is null)
            {
                throw new UnreachableException($"Authentication state not set in {nameof(RevalidatingServerAuthenticationStateProvider)}.{nameof(OnPersistingAsync)}().");
            }

            var authenticationState = await _authenticationStateTask;
            var principal = authenticationState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(_options.ClaimsIdentity.UserIdClaimType)?.Value;
                var name = principal.FindFirst("name")?.Value;
                var roles = principal.FindAll("email").Select(s => s.Value).ToArray();

                if (userId != null && name != null)
                {
                    _state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        //UserId = userId,
                        Name = name,
                        Roles = roles
                    });
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            _subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
            base.Dispose(disposing);
        }
    }

    public record UserInfo(string Name = null!, string[] Roles = null!);

}
