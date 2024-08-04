using BTS.Domain.Constants;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Email;
using BTS.Domain.Contractors.Repositories;
using BTS.Infrastructure.Authentication;
using BTS.Infrastructure.Databases.Contexts;
using BTS.Infrastructure.Databases.Repositories;
using BTS.Infrastructure.Email;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BTS.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration);
            services.AddRBACAuthorization();
            services.AddDbContext(configuration);
            services.AddRepositories();
            services.AddEmail(configuration);
        }

        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<BTSDbContext>(context => context.UseSqlServer(configuration.GetConnectionString("MigrationDb")));

        private static void AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IDriverRepository, DriverRepository>()
                    .AddScoped<IBusRepository, BusRepository>()
                    .AddScoped<IRouteRepository, RouteRepository>()
                    .AddScoped<IUserRepository, UserRepository>();

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Initialize Jwt Configuration
            var jwtSetting = JwtSetting.FromConfiguration(configuration);
            services.AddSingleton(jwtSetting);
            services.AddScoped<IJwtProvider, JwtProvider>();

            // Setup Jwt Bearer Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => options.TokenValidationParameters = jwtSetting.TokenValidationParameters);

        }

        public static void AddRBACAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Common.AUTHORIZE_ROLE_ADMIN, policy =>
                {
                    policy.RequireClaim(Common.CLAIM_NAME_USER_ROLE, Common.CLAIM_VALUE_ROLE_ADMIN);
                });
                options.AddPolicy(Common.AUTHORIZE_ROLE_USER, policy =>
                {
                    policy.RequireClaim(Common.CLAIM_NAME_USER_ROLE, Common.CLAIM_VALUE_ROLE_USER);
                });
            });
        }

        public static void AddEmail(this IServiceCollection services, IConfiguration configuration)
        {
            // Initialize Email Configuration
            var emailSetting = EmailSetting.FromConfiguration(configuration);
            services.AddSingleton(emailSetting);
            services.AddScoped<IEmailProvider, EmailProvider>();
        }
    }
}
