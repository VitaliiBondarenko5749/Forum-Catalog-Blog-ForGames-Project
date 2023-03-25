namespace Forum_BAL.DTO
{
    public class ConcretePostInfoDTO
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CreatedAt { get; set; }
        public ICollection<ShortGameInfoDTO>? Games { get; set; }
        public ICollection<CommentInfoDTO>? Comments { get; set; }
    }
}
