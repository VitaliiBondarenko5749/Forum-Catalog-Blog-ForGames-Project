using AutoMapper.Configuration.Annotations;

namespace Forum_DAL.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public DateTime WhenReplied { get; set; }
        
        [Ignore]
        public int NumberOfLikes { get; set; }
    }
}