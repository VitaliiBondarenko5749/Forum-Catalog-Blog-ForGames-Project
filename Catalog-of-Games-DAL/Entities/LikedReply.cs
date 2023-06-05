namespace Catalog_of_Games_DAL.Entities
{
    public class LikedReply
    {
        public Guid Id { get; set; }

        public Reply Reply { get; set; } = null!;
        public Guid ReplyId { get; set; }

        public Guid UserId { get; set; }
    }
}