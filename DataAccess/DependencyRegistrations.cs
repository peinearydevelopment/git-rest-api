namespace PinaryDevelopment.Git.Server.DataAccess
{
    using Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepositoriesDal, RepositoriesDal>();
            services.AddSingleton<IGitProcessFactory, GitProcessFactory>();
            services.AddSingleton<IDirectoryHelper, LocalFileSystemDirectoryHelper>();
            services.AddOptions<GitConfigurationOptions>();

            return services;
        }
    }
}
