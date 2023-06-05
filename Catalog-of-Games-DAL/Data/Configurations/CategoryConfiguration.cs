using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog_of_Games_DAL.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(c => c.Icon)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.ToTable("Categories", "gamecatalog");

            builder.HasData(DataSeeder.Categories);
        }
    }
}