using Catalog_of_Games_BAL.DTOs;

namespace Catalog_of_Games_BAL.Contracts
{
    public interface IDeveloperService
    {
        // Додаємо нового розробника до бази даних
        Task AddAsync(DeveloperInsertDto developerDto);

        // Видаляємо розробника з  бази даних
        Task DeleteAsync(Guid id);

        // Знаходимо розробників за іменем
        Task<List<string>> FindByNameAsync(string developerName);
    }
}