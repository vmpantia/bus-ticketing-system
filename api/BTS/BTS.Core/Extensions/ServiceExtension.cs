using BTS.Core.Commands.Models;
using BTS.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BTS.Core.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCore(this IServiceCollection services) =>
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                    .AddAutoMapper(Assembly.GetExecutingAssembly())
                    .AddScoped<IValidator<CreateDriverCommand>, CreateDriverCommandValidator>();
    }
}
