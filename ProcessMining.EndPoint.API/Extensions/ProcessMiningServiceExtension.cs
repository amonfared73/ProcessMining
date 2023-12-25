using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Queries;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.ApplicationService.Services.Authenticators;
using ProcessMining.Core.ApplicationService.TokenGenerators;
using ProcessMining.Core.ApplicationService.TokenValidators;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Infra.EntityFramework.DbContextes;
using ProcessMining.Infra.Tools.Reflections;

namespace ProcessMining.EndPoint.API.Extensions
{
    public static class ProcessMiningServiceExtension
    {
        public static IServiceCollection AddProcessMiningDbContext(this IServiceCollection services)
        {
            var connectionString = "Data Source=ProcessMining.db";
            services.AddDbContext<ProcessMiningDbContext>(options => options.UseSqlite(connectionString), optionsLifetime: ServiceLifetime.Singleton);
            services.AddDbContextFactory<ProcessMiningDbContext, ProcessMiningDbContextFactory>(options => options.UseSqlite(connectionString));
            return services;
        }

        public static IServiceCollection AddAccessTokenGenerator(this IServiceCollection services)
        {
            services.AddSingleton<AccessTokenGenerator>();
            services.AddSingleton<RefreshTokenGenerator>();
            services.AddSingleton<RefreshTokenValidator>();
            services.AddSingleton<Authenticator>();
            services.AddSingleton<TokenGenerator>();
            return services;
        }

        public static IServiceCollection AddProcessMiningServices(this IServiceCollection services)
        {
            // Register base services
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            // Get all services corresponding to Registration Required Attribute
            var repositoryTypes = Assemblies.GetServices("ProcessMining.Core.ApplicationService", typeof(RegistrationRequiredAttribute));

            // Register each service
            foreach (var repositoryType in repositoryTypes)
            {
                var repositoryInterface = repositoryType.GetInterfaces().Where(i => !i.IsGenericType).FirstOrDefault();
                services.AddTransient(repositoryInterface, repositoryType);
            }

            // Return extension method value
            return services;
        }
    }
}
