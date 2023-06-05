using Catalog_of_Games_BAL.DTOs;

namespace Catalog_of_Games_BAL.Contracts
{
    public interface IPlatformService
    {
        Task AddAsync(PlatformInsertDto platformDto);
        Task DeleteAsync(Guid id);
        Task<List<string>> FindByNameAsync(string platformName);
    }
}