namespace Catalog_of_Games_DAL.Entities
{
    public class Reply
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }

        public ICollection<CommentReply>? CommentsReplies { get; set; }

        public ICollection<LikedReply>? LikedReplies { get; set; }
    }
}