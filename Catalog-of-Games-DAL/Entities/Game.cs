using AutoMapper.Configuration.Annotations;

namespace Catalog_of_Games_DAL.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int PublisherId { get; set; }
        public float Rating { get; set; }
        public string? Description { get; set; }
        public string? MainImage { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
    }
}
