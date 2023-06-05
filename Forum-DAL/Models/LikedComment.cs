namespace Forum_DAL.Models
{
    public class LikedComment
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
    }
}