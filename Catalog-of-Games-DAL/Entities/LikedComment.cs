namespace Catalog_of_Games_DAL.Entities
{
    public class LikedComment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public GameComment? GameComment { get; set; }
        public Guid CommentId { get; set; }
    }
}