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
            services.AddBlazoredLocalStorage();

            services.AddScoped<AddHeadersDelegatingHandler>();
            services.AddScoped(sp =>
            {
                var handler = sp.GetRequiredService<AddHeadersDelegatingHandler>();
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://api45gabs.azurewebsites.net/")
                };
            });

            return services;
        }
    }
}
