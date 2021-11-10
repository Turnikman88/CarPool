using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.DataConfigurations
{
    class ProfilePictureConfig : IEntityTypeConfiguration<ProfilePicture>
    {
        public void Configure(EntityTypeBuilder<ProfilePicture> builder)
        {
            builder.HasIndex(e => e.ApplicationUserId)
                    .IsUnique();

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.HasOne(d => d.ApplicationUser)
                .WithOne(p => p.ProfilePicture)
                .HasForeignKey<ProfilePicture>(d => d.ApplicationUserId);
        }
    }
}
