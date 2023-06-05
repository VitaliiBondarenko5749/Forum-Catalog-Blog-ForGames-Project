using Catalog_of_Games_BAL.DTOs;

namespace Catalog_of_Games_BAL.Contracts
{
    public interface ICategoryService
    {
        // Отримуємо всі категорії по списку.
        Task<List<CategoryInfoDto>> GetAllCategoriesAsync();

        // Додаємо категорію
        Task AddAsync(CategoryInsertDto categoryDto);

        // Оновлюємо категорію
        Task UpdateAsync(Guid id, CategoryInsertDto categoryDto);

        // Видаляємо категорію
        Task DeleteAsync(Guid id);

        // Знаходимо категорії за іменем
        Task<List<string>> FindByNameAsync(string categoryName);
    }
}