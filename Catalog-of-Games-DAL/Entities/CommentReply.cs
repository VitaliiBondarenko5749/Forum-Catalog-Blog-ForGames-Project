namespace Catalog_of_Games_DAL.Entities
{
    public class CommentReply
    {
        public Guid Id { get; set; }

        public GameComment Comment { get; set; } = null!;
        public Guid CommentId { get; set; }

        public Reply Reply { get; set; } = null!;
        public Guid ReplyId { get; set; }
    }
}