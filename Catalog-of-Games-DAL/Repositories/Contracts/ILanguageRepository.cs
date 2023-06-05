using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface ILanguageRepository : IGenericRepository<Language>
    {
        // Знаходимо мови за іменем
        Task<List<string>> FindManyByNameAsync(string name);
        Task<Language?> FindByNameAsync(string name);
    }
}