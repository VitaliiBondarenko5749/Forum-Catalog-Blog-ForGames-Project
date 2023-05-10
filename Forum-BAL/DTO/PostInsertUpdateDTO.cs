namespace Forum_BAL.DTO
{
    public class PostInsertUpdateDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public ICollection<ShortGameInfoDTO>? Games { get; set; }
    }
}