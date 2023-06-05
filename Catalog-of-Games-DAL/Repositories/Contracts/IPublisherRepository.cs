using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task<Publisher?> FindByNameAsync(string name);
        Task DeleteByNameAsync(string name);
    }
}