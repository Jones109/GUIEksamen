using System;
using Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<TreeTypeToMeasure> TreeTypesToMeasure { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EksamenDB;Trusted_Connection=true;MultipleActiveResultSets=true");

            }
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TreeTypeToMeasure>()
                .HasOne<Location>(t => t.Location)
                .WithMany(l => l.TreeTypesToMeasure)
                .HasForeignKey(t => t.LocationId);

            modelBuilder.Entity<Sensor>()
                .HasOne<Location>(s => s.Location)
                .WithMany(l => l.Sensors)
                .HasForeignKey(s => s.LocationId);


        }
    }
}
