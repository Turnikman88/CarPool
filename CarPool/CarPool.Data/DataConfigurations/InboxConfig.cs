using CarPool.Data.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarPool.Data.DataConfigurations
{
    class InboxConfig : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.HasIndex(e => e.ApplicationUserId);

            builder.HasOne(d => d.ApplicationUser)
                .WithMany(p => p.InboxMessages)
                .HasForeignKey(d => d.ApplicationUserId);
        }
    }
}
