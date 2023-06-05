using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder.HasIndex(d => d.Name)
                .IsUnique();

            builder.ToTable("Developers", "gamecatalog");

            builder.HasData(DataSeeder.Developers);
        }
    }
}