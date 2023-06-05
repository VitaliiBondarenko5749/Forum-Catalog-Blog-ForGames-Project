using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }

        // Знаходимо мови за іменем
        public async Task<List<string>> FindManyByNameAsync(string name)
        {
            return await dbContext.Languages.AsNoTracking()
                .Where(l => l.Name.Contains(name))
                .Select(l => l.Name)
                .ToListAsync();
        }

        public async Task<Language?> FindByNameAsync(string name)
        {
            return await dbContext.Languages.AsNoTracking()
                .FirstOrDefaultAsync(l => l.Name.Equals(name));
        }
    }
}