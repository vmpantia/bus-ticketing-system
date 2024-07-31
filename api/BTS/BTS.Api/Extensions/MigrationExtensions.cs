using BTS.Infrastructure.Databases.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BTS.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using BTSDbContext context = scope.ServiceProvider.GetRequiredService<BTSDbContext>();
            context.Database.Migrate();
        }
    }
}
