using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<List<Game>> GetGamesByCategoryAsync(string categoryName, int pageNumber, int pageSize);
        Task<Game> GetGameByNameAsync(string categoryName, string gameName);
        Task<List<Game>> FindByNameAsync(int pageNumber, int pageSize, string gameName);
    }
}