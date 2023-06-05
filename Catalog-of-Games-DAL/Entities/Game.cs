namespace Catalog_of_Games_DAL.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public Guid PublisherId { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; } = null!;
        public string MainImage { get; set; } = null!;

        public Publisher? Publisher { get; set; }

        public ICollection<GameCategory>? GameCategories { get; set; }

        public ICollection<GameDeveloper>? GameDevelopers { get; set; }

        public ICollection<GamePlatform>? GamePlatforms { get; set; }

        public ICollection<GameLanguage>? GameLanguages { get; set; }

        public ICollection<GameComment>? GameComments { get; set; }

        public ICollection<GameImage>? GameImages { get; set; }
    }
}