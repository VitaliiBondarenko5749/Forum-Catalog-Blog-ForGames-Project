using AutoMapper.Configuration.Annotations;
using Catalog_of_Games_DAL.Entities;

namespace Forum_DAL.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        [Ignore]
        public ICollection<Game>? Games { get; set; }

        [Ignore]
        public ICollection<Comment>? Comments { get; set; }
    }
}
