using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class GameImageConfiguration : IEntityTypeConfiguration<GameImage>
    {
        public void Configure(EntityTypeBuilder<GameImage> builder)
        {
            builder.HasKey(gi => gi.Id);

            builder.Property(gi => gi.GameId)
                .IsRequired();

            builder.HasOne(gi => gi.Game)
                .WithMany(g => g.GameImages)
                .HasForeignKey(gi => gi.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("GamesImages", "gamecatalog");

            builder.HasData(DataSeeder.GamesImages);
        }
    }
}