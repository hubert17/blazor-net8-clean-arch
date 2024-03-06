using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorNet8CleanArch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped(sp => new HttpClient //(new AddHeadersDelegatingHandler())
            {
                BaseAddress = new Uri("https://api45gabs.azurewebsites.net/")
            });

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddAuthorizationCore();

            services.AddBlazoredLocalStorage();

            // Entity Framework and other 3rd party library
            return services;
        }
    }
}
