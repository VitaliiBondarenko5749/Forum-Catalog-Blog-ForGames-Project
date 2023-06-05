using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class GameCommentConfiguration : IEntityTypeConfiguration<GameComment>
    {
        public void Configure(EntityTypeBuilder<GameComment> builder)
        {
            builder.HasKey(gc => gc.Id);

            builder.HasOne(gc => gc.Game)
                .WithMany(g => g.GameComments)
                .HasForeignKey(gc => gc.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(gc => gc.Content)
                .HasMaxLength(200)
                .IsRequired();

            builder.ToTable("GamesComments", "gamecatalog");

            builder.HasData(DataSeeder.GamesComments);
        }
    }
}