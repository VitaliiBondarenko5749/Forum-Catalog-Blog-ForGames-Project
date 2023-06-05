namespace Catalog_of_Games_BAL.DTOs
{
    public class GameInsertDto
    {
        public string Name { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public float Rating { get; set; }
        public string? Description { get; set; } = null!;
        public ICollection<string> Categories { get; set; } = null!;
        public ICollection<string> Developers { get; set; } = null!;
        public ICollection<string> Platforms { get; set; } = null!;
        public ICollection<string> Languages { get; set; } = null!;
    }
}