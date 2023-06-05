using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class LikedCommentConfiguration : IEntityTypeConfiguration<LikedComment>
    {
        public void Configure(EntityTypeBuilder<LikedComment> builder)
        {
            builder.HasKey(lc => lc.Id);

            builder.Property(lc => lc.UserId)
                .IsRequired();

            builder.Property(lc => lc.CommentId)
                .IsRequired();

            builder.HasOne(lc => lc.GameComment)
                .WithMany(gc => gc.LikedComments)
                .HasForeignKey(lc => lc.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("LikedComments", "gamecatalog");

            builder.HasData(DataSeeder.LikedComments);
        }
    }
}