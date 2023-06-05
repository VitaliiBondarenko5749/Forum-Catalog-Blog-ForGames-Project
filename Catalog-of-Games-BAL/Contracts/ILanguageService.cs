using Catalog_of_Games_BAL.DTOs;

namespace Catalog_of_Games_BAL.Contracts
{
    public interface ILanguageService
    {
        Task AddAsync(LanguageInsertDto languageDto);
        Task DeleteAsync(Guid id);
        Task<List<string>> FindByNameAsync(string languageName);
    }
}