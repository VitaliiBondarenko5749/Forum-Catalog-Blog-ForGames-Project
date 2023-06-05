namespace Catalog_of_Games_DAL.Entities
{
    public class Developer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<GameDeveloper>? GameDevelopers { get; set; }
    }
}