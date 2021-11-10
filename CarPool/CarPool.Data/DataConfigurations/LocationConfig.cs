using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.DataConfigurations
{
    class LocationConfig : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(p => p.Latitude)
                        .HasColumnType("decimal(18,4)");

            builder.Property(p => p.Longitude)
                        .HasColumnType("decimal(18,4)");
        }            
    }
}
