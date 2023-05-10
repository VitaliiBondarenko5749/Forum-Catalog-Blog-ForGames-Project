namespace Forum_BAL.DTO
{
    public class ShortPostInfoDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? CreatedAt { get; set; }
        public ICollection<ShortGameInfoDTO>? Games { get; set; }
    }
}