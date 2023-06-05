namespace Catalog_of_Games_BAL.DTOs
{
    public class GameInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public float Rating { get; set; }
        public string Description { get; set; } = null!;
        public PublisherInfoDto Publisher { get; set; } = null!;
        public ICollection<ShortCategoryInfoDto> Categories { get; set; } = null!;
        public ICollection<DeveloperInfoDto> Developers { get; set; } = null!;
        public ICollection<LanguageInfoDto> Languages { get; set; } = null!;
        public ICollection<PlatformInfoDto> Platforms { get; set; } = null!;
    }
}