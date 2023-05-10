namespace Forum_DAL.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        public Guid PublisherId { get; set; }
        public float Rating { get; set; }
        public string? Description { get; set; }
        public string MainImage { get; set; } = default!;
    }
}