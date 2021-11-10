using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasIndex(e => e.ApplicationRoleId);

            builder.HasOne(d => d.ApplicationRole)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.ApplicationRoleId);

            builder.HasOne(o => o.Address)
                .WithMany(m => m.ApplicationUsers)
                .HasForeignKey(f => f.AddressId);

            builder.Property(e => e.Password).IsRequired();

            builder.HasCheckConstraint("Password_contains_space", "Password NOT LIKE '% %'");

            builder.Property(e => e.Email).IsRequired();

            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.FirstName).IsRequired();

            builder.Property(e => e.LastName).IsRequired();

            builder.Property(e => e.PhoneNumber).IsRequired();

            builder.HasIndex(e => e.PhoneNumber).IsUnique();

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
