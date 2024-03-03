using Microsoft.Extensions.DependencyInjection;

namespace BlazorNet8CleanArch.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // services.AddScoped<IRepository, Repository>();
            return services;
        }
    }
}
