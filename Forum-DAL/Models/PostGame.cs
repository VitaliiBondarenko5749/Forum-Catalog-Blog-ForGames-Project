namespace Forum_DAL.Models
{
    public class PostGame
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid GameId { get; set; }
    }
}