using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_BAL.Contracts
{
    public interface IGameService
    {
        // Отримуємо всі ігри конкретної категорії
        Task<List<ShortGameInfoDto>> GetGamesByCategoryAsync(string categoryName, int pageNumber, int pageSize);

        // Отримуємо інформацію про конкретну гру(шукаємо за іменем)
        Task<GameInfoDto> GetGameByNameAsync(string categoryName, string gameName);

        // Отримуємо всі можливі ігри
        Task<List<ShortGameInfoDto>> GetAllGamesAsync(int pageNumber, int pageSize);

        // Отримуємо всі можливі ігри за іменем
        Task<List<ShortGameInfoDto>> FindByNameAsync(int pageNumber, int pageSize, string gameName);

        // Додаємо гру
        Task AddAsync(GameInsertDto gameDto);

        // Оновлюємо гру
        Task UpdateAsync(Guid id, GameInsertDto gameDto);
    }
}