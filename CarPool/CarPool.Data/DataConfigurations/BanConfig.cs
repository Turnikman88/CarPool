using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    public class BanConfig : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.HasIndex(e => e.ApplicationUserId)
                    .IsUnique();

            builder.HasOne(d => d.ApplicationUser)
                .WithOne(p => p.Ban)
                .HasForeignKey<Ban>(d => d.ApplicationUserId);
        }
    }
}
