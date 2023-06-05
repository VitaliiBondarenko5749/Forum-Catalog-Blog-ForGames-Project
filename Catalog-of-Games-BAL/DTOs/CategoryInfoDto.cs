namespace Catalog_of_Games_BAL.DTOs
{
    public class CategoryInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}