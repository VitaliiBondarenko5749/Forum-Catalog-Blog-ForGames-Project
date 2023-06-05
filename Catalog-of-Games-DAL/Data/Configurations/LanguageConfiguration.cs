using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder.HasIndex(l => l.Name)
                .IsUnique();

            builder.ToTable("Languages", "gamecatalog");

            builder.HasData(DataSeeder.Languages);
        }
    }
}