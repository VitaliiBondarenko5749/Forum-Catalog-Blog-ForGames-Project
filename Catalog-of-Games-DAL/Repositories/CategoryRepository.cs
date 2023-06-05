using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }

        public async Task<List<string>> FindManyByNameAsync(string name)
        {
            return await dbContext.Categories.AsNoTracking()
                .Where(c => c.Name.Contains(name))
                .Select(c => c.Name)
                .ToListAsync();
        }

        public async Task<Category?> FindByNameAsync(string name)
        {
            return await dbContext.Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name.Equals(name));
        }
    }
}