using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BTS.Infrastructure.Databases.Contexts
{
    public class BTSDbContext : DbContext
    {
        private readonly List<User> _initialUsers;
        public BTSDbContext(DbContextOptions options) : base(options)
        {
            _initialUsers = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Username = "admin",
                    Email = "test_admin@test.com",
                    Password = "P@ssw0rd",
                    FirstName = "Admin",
                    LastName = "Admin",
                    IsEmailConfirmed = true,
                    IsAdmin = true,
                    Status = CommonStatus.Active,
                    CreatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc(),
                    CreatedBy = "System"
                }
            };
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>(b =>
            {
                b.HasQueryFilter(data => data.Status != CommonStatus.Deleted);

                b.HasOne(b => b.Driver)
                 .WithOne(d => d.Bus)
                 .IsRequired(false);

                b.HasOne(b => b.Route)
                 .WithOne(r => r.Bus)
                 .IsRequired(false);
            });

            modelBuilder.Entity<Driver>(d =>
            {
                d.HasQueryFilter(data => data.Status != CommonStatus.Deleted);

                d.HasOne(d => d.Bus)
                 .WithOne(b => b.Driver)
                 .IsRequired(false);
            });

            modelBuilder.Entity<Route>(r =>
            {
                r.HasQueryFilter(data => data.Status != CommonStatus.Deleted);

                r.HasOne(r => r.Bus)
                 .WithOne(b => b.Route)
                 .IsRequired(false);

                r.HasOne(r => r.OriginTerminal)
                 .WithMany(t => t.OriginRoutes)
                 .HasForeignKey(r => r.OriginTerminalId)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();

                r.HasOne(r => r.DestinationTerminal)
                 .WithMany(t => t.DestinationRoutes)
                 .HasForeignKey(r => r.DestinationTerminalId)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();
            });

            modelBuilder.Entity<Terminal>(t =>
            {
                t.HasQueryFilter(data => data.Status != CommonStatus.Deleted);

                t.HasMany(t => t.OriginRoutes)
                 .WithOne(r => r.OriginTerminal)
                 .HasForeignKey(r => r.OriginTerminalId)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();

                t.HasMany(t => t.DestinationRoutes)
                 .WithOne(r => r.DestinationTerminal)
                 .HasForeignKey(r => r.DestinationTerminalId)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();
            });

            modelBuilder.Entity<User>(u =>
            {
                u.HasQueryFilter(data => data.Status != CommonStatus.Deleted);

                u.HasIndex(u => new { u.Username, u.Email, u.Password, u.IsEmailConfirmed, u.IsAdmin });

                u.HasData(_initialUsers);
            });

            modelBuilder.Entity<AccessToken>(a =>
            {
                a.HasIndex(a => new { a.UserId, a.Token, a.IsUsed });
            });
        }
    }
}
