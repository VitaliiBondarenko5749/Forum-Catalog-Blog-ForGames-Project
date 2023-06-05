namespace Catalog_of_Games_DAL.Entities
{
    public class GameComment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }

        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<LikedComment>? LikedComments { get; set; }
        public ICollection<CommentReply>? CommentsReplies { get; set; }
    }
}