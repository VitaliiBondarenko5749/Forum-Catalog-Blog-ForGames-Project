using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<string>> FindManyByNameAsync(string name);   
        Task<Category?> FindByNameAsync(string name);
    }
}