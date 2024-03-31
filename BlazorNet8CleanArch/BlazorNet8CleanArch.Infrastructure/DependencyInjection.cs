using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Authentication;
using BlazorNet8CleanArch.Infrastructure.Handlers.BlazorStateHandler;
using BlazorState;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorNet8CleanArch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();

            services.AddScoped<CustomHttpHandler>();
            services.AddScoped(sp =>
            {
                var handler = sp.GetRequiredService<CustomHttpHandler>();
                return new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://api45gabs.azurewebsites.net")
                };
            });

            services.AddBlazorState(options => options.Assemblies = [typeof(CounterState).GetTypeInfo().Assembly]);

            return services;
        }
    }
}
