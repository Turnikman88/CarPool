using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    class TripPassengerConfig : IEntityTypeConfiguration<TripPassenger>
    {
        public void Configure(EntityTypeBuilder<TripPassenger> builder)
        {
            builder.HasKey(k => new { k.ApplicationUserId, k.TripId });

            builder.HasOne(d => d.ApplicationUser)
                .WithMany(x => x.TripsAsPassenger)
                .HasForeignKey(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripPassengerRelation_ApplicationUsers");

            builder.HasOne(d => d.Trip)
                .WithMany(x => x.Passengers)
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripPassengerRelation_Trips");
        }
    }
}
