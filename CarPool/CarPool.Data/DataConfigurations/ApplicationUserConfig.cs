using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.DataConfigurations
{
    class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasOne(o => o.ProfilePicture)
                .WithOne(o => o.ApplicationUser)
                .HasForeignKey<ProfilePicture>(f => f.ApplicationUserId);

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

            builder.HasQueryFilter(x => !x.IsDeleted);  
        }    
    }
}
