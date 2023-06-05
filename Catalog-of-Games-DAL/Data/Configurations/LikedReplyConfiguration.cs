using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class LikedReplyConfiguration : IEntityTypeConfiguration<LikedReply>
    {
        public void Configure(EntityTypeBuilder<LikedReply> builder)
        {
            builder.HasKey(lr => lr.Id);

            builder.Property(lr => lr.ReplyId)
                .IsRequired();

            builder.Property(lr => lr.UserId)
                .IsRequired();

            builder.HasOne(lr => lr.Reply)
                .WithMany(r => r.LikedReplies)
                .HasForeignKey(lr => lr.ReplyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("LikedReplies", "gamecatalog");

            builder.HasData(DataSeeder.LikedReplies);
        }
    }
}