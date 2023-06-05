using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(g => g.ReleaseDate)
                .IsRequired();

            builder.Property(g => g.PublisherId)
                .IsRequired();

            builder.Property(g => g.Rating)
                .IsRequired();

            builder.Property(g => g.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(g => g.MainImage)
                .IsRequired();

            builder.HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(g => g.Name)
                .IsUnique();

            builder.ToTable("Games", "gamecatalog");

            builder.HasData(DataSeeder.Games);
        }
    }
}