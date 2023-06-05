namespace Catalog_of_Games_DAL.Entities
{
    public class GameImage
    {
        public Guid Id { get; set; }

        public Game Game { get; set; } = null!;
        public Guid GameId { get; set; }

        public string Directory { get; set; } = null!;
    }
}