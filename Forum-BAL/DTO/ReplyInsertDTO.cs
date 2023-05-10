namespace Forum_BAL.DTO
{
    public class ReplyInsertDTO
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public string? Content { get; set; }
    }
}