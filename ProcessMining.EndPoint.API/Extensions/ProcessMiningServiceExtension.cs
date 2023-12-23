using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Queries;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Infra.EntityFramework.DbContextes;

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

        public static IServiceCollection AddProcessMiningServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
