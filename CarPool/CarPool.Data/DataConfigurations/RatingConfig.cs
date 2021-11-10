using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    class RatingConfig : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasIndex(e => e.ApplicationUserId);

            builder.HasOne(d => d.ApplicationUser)
                .WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ApplicationUserId);
        }
    }
}
