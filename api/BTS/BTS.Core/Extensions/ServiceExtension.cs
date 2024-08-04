using BTS.Core.Behaviors;
using BTS.Core.MessageBroker;
using BTS.Core.Services;
using BTS.Domain.Contractors.Authentication;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BTS.Core.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper();
            services.AddValidators();
            services.AddMediatR();
            services.AddMessageBroker(configuration);
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
                config.AddOpenBehavior(typeof(TransactionPipelineBehavior<,>));
            });

        private static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            var messageBrokerSetting = MessageBrokerSetting.FromConfiguration(configuration);
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();
                config.AddConsumers(Assembly.GetExecutingAssembly());
                config.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri(messageBrokerSetting.Host), auth =>
                    {
                        auth.Username(messageBrokerSetting.User);
                        auth.Password(messageBrokerSetting.Password);
                    });

                    config.ConfigureEndpoints(context);
                });
            });
        }

        private static void AddServices(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationService, AuthenticationService>()
                    .AddScoped<IEmailService, EmailService>();
    }
}
