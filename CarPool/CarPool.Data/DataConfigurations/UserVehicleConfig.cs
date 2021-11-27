using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.DataConfigurations
{
    class UserVehicleConfig : IEntityTypeConfiguration<UserVehicle>
    {
        public void Configure(EntityTypeBuilder<UserVehicle> builder)
        {
            builder.Property(x => x.Model).IsRequired();
            builder.Property(x => x.Color).IsRequired();
            builder.Property(x => x.FuelConsumptionPerHundredKilometers).IsRequired();

            builder.HasIndex(e => e.ApplicationUserId)
                    .IsUnique();

            builder.HasOne(d => d.ApplicationUser)
                .WithOne(p => p.Vehicle)
                .HasForeignKey<UserVehicle>(d => d.ApplicationUserId);
        }
    }
}
