﻿namespace CarPool.Data
{
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using CarPool.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public partial class CarPoolDBContext : DbContext
    {
        public CarPoolDBContext()
        {
        }

        public CarPoolDBContext(DbContextOptions<CarPoolDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }      
        
        public virtual DbSet<Location> Locations { get; set; }
        
        public virtual DbSet<ProfilePicture> ProfilePictures { get; set; } 
        
        public virtual DbSet<Rating> Ratings { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                        .Property(p => p.Latitude).
                        HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Location>()
                        .Property(p => p.Longitude)
                        .HasColumnType("decimal(18,4)");

            // modelBuilder.Seed();
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        // For soft delete
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }
              
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }
    }
}