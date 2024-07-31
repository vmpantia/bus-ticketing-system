using BTS.Core.Behaviors;
using BTS.Core.Commands.Models;
using BTS.Core.Results;
using BTS.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BTS.Core.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCore(this IServiceCollection services) =>
            services.AddAutoMapper()
                    .AddMediatR()
                    .AddValidators();

        private static IServiceCollection AddAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

        private static IServiceCollection AddValidators(this IServiceCollection services) =>
            services.AddScoped<IValidator<CreateDriverCommand>, CreateDriverCommandValidator>();

        private static IServiceCollection AddMediatR(this IServiceCollection services) =>
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidatorBehavior<,>));
                config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
            });

    }
}
