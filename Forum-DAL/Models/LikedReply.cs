namespace Forum_DAL.Models
{
    public class LikedReply
    {
        public Guid Id { get; set; }
        public Guid ReplyId { get; set; }
        public Guid UserId { get; set; }
    }
}