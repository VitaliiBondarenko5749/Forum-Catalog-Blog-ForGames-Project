namespace Forum_BAL.DTO
{
    public class CommentInsertDTO
    {    
        public Guid PostId { get; set; }
        public string? Content { get; set; }
    }
}