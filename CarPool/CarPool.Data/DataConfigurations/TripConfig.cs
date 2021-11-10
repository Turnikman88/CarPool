using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.DataConfigurations
{
    class TripConfig : IEntityTypeConfiguration<Trips>
    {
        public void Configure(EntityTypeBuilder<Trips> builder)
        {
            builder.HasIndex(x => x.DriverId);

            builder.HasOne(o => o.Applicationuser).WithMany(m => m.Trips).HasForeignKey(f => f.ApplicationuserId);
        }
    }
}
