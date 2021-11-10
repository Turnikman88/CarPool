using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CarPool.Data.DataConfigurations
{
    class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasIndex(x => x.CityId);

            builder.Property(e => e.StreetName).IsRequired();

            builder.HasOne(d => d.City)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId);

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(p => p.Latitude)
                        .HasColumnType("decimal(18,4)");

            builder.Property(p => p.Longitude)
                        .HasColumnType("decimal(18,4)");
        }            
    }
}
