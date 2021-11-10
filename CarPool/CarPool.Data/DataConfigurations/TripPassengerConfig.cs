using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.DataConfigurations
{
    class TripPassengerConfig : IEntityTypeConfiguration<TripPassenger>
    {
        public void Configure(EntityTypeBuilder<TripPassenger> builder)
        {
            builder.HasKey(k => new { k.TripId, k.ApplicationUserId });

            builder.HasOne(k=> k.Trip).WithMany(m=>m.)
        }
    }
}
