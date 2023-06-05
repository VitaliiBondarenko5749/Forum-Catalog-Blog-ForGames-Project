using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface IPlatformRepository : IGenericRepository<Platform>
    {
        Task<List<string>> FindManyByNameAsync(string name);
        Task<Platform?> FindByNameAsync(string name);
    }
}