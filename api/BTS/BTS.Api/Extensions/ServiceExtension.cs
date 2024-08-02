using BTS.Domain.Constants;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace BTS.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Schema Definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Please provide access token"
                });

                // Apply Schema Globally
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });
        }

        public static void AddRBACAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Common.AUTHORIZE_ROLE_ADMIN, policy => policy.RequireClaim(Common.CLAIM_NAME_ROLE, Common.CLAIM_VALUE_ROLE_ADMIN));
                options.AddPolicy(Common.AUTHORIZE_ROLE_USER, policy => policy.RequireClaim(Common.CLAIM_NAME_ROLE, Common.CLAIM_VALUE_ROLE_ADMIN));
            });
        }
    }
}
