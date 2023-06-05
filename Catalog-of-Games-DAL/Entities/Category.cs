namespace Catalog_of_Games_DAL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Icon { get; set; } = null!;

        public ICollection<GameCategory>? GameCategories { get; set; }
    }
}