namespace CarPool.Data
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
        
        public virtual DbSet<Address> Addresses { get; set; }
        
        public virtual DbSet<ProfilePicture> ProfilePictures { get; set; } 
        
        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<Trips> Trips { get; set; }

        public virtual DbSet<TripPassenger> TripPassengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Seed();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
