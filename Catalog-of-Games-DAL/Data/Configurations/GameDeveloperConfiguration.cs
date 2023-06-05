using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class GameDeveloperConfiguration : IEntityTypeConfiguration<GameDeveloper>
    {
        public void Configure(EntityTypeBuilder<GameDeveloper> builder)
        {
            builder.HasKey(gc => gc.Id);

            builder.HasOne(gd => gd.Game)
                .WithMany(g => g.GameDevelopers)
                .HasForeignKey(gd => gd.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gd => gd.Developer)
                .WithMany(d => d.GameDevelopers)
                .HasForeignKey(gd => gd.DeveloperId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("GamesDevelopers", "gamecatalog");

            builder.HasData(DataSeeder.GamesDevelopers);
        }
    }
}