using BTS.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTS.Infrastructure.Databases.Contexts
{
    public class BTSDbContext : DbContext
    {
        public BTSDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Terminal> Terminals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>(b =>
            {
                b.HasOne(b => b.Driver)
                 .WithOne(d => d.Bus)
                 .IsRequired();
            });

            modelBuilder.Entity<Driver>(d =>
            {
                d.HasOne(d => d.Bus)
                 .WithOne(b => b.Driver)
                 .IsRequired();
            });

            modelBuilder.Entity<Route>(r =>
            {
                r.HasOne(r => r.Bus)
                 .WithMany(b => b.Routes)
                 .HasForeignKey(r => r.BusId)
                 .IsRequired();

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
        }
    }
}
