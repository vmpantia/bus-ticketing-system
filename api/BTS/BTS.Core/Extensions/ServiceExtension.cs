using BTS.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BTS.Core.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCore(this IServiceCollection services) =>
            services.AddAutoMapper()
                    .AddValidators()
                    .AddMediatR();

        private static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        private static IServiceCollection AddValidators(this IServiceCollection services) =>
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        private static IServiceCollection AddMediatR(this IServiceCollection services) =>
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                config.AddOpenBehavior(typeof(UnitOfWorkPipelineBehavior<,>));
            });

    }
}
