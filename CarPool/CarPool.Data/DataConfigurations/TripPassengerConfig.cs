using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    class TripPassengerConfig : IEntityTypeConfiguration<TripPassenger>
    {
        public void Configure(EntityTypeBuilder<TripPassenger> builder)
        {
            builder.HasNoKey();

            builder.HasOne(d => d.ApplicationUser)
                .WithMany()
                .HasForeignKey(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripPassengerRelation_ApplicationUsers");

            builder.HasOne(d => d.Trip)
                .WithMany()
                .HasForeignKey(d => d.TripId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TripPassengerRelation_Trips");
        }
    }
}
