namespace Forum_DAL.Models
{
    public class CommentReply
    {
        public Guid Id { get; set; }
        public Guid CommentId { get; set; }
        public Guid ReplyId { get; set; }
    }
}