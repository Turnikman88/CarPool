using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasIndex(x => x.DriverId);

            builder.Property(p => p.Price)
                        .HasColumnType("decimal(18,2)");

            builder.HasOne(d => d.DestinationAddress)
                    .WithMany(p => p.TripsDestinationAddress)
                    .HasForeignKey(d => d.DestinationAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trips_DestinationAddresses");

            builder.HasOne(d => d.StartAddress)
                    .WithMany(p => p.TripsStartAddress)
                    .HasForeignKey(d => d.StartAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trips_StartAddresses");

            builder.HasOne(d => d.Driver)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trips_ApplicationUsers");
        }
    }
}
