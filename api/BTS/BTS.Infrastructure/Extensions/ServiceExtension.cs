using BTS.Domain.Contractors.Repositories;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTS.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext(configuration)
                    .AddRepositories();

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<BTSDbContext>(context => context.UseSqlServer(configuration.GetConnectionString("MigrationDb")));

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IDriverRepository, DriverRepository>();
    }
}
