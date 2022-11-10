namespace PinaryDevelopment.Git.Server.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using Contracts;

    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepositoriesService, RepositoriesService>();

            return services;
        }
    }
}
