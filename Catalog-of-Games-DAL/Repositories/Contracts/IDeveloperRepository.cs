using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface IDeveloperRepository : IGenericRepository<Developer>
    {
        Task<List<string>> FindManyByNameAsync(string name);
        Task<Developer?> FindByNameAsync(string name);
    }
}