namespace Catalog_of_Games_DAL.Entities
{
    public class GamePlatform
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public Guid PlatformId { get; set; }
        public Platform? Platform { get; set; }
    }
}