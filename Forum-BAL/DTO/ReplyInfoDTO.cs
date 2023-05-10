namespace Forum_BAL.DTO
{
    public class ReplyInfoDTO
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string? WhenReplied { get; set; }
        public int NumberOfLikes { get; set; }
    }
}