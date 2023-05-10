using AutoMapper.Configuration.Annotations;

namespace Forum_DAL.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }

        [Ignore]
        public ICollection<Game>? Games { get; set; }

        [Ignore]
        public ICollection<Comment>? Comments { get; set; }
    }
}