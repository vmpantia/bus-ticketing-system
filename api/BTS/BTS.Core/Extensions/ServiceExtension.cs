using BTS.Core.Behaviors;
using BTS.Core.Services;
using BTS.Domain.Contractors.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BTS.Core.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddValidators();
            services.AddMediatR();
            services.AddServices();
        }

        private static void AddAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        private static void AddValidators(this IServiceCollection services) =>
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        private static void AddMediatR(this IServiceCollection services) =>
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(UnitOfWorkPipelineBehavior<,>));
            });

        private static void AddServices(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}
