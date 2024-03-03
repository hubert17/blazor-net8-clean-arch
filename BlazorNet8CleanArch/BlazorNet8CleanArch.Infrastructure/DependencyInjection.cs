using Microsoft.Extensions.DependencyInjection;

namespace BlazorNet8CleanArch.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Entity Framework and other 3rd party library
            return services;
        }
    }
}
