namespace Forum_DAL.Models
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
    }
}