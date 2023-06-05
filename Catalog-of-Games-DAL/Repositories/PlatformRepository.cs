using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class PlatformRepository : GenericRepository<Platform>, IPlatformRepository
    {
        public PlatformRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }

        public async Task<List<string>> FindManyByNameAsync(string name)
        {
            return await dbContext.Platforms.AsNoTracking()
                .Where(p => p.Name.Contains(name))
                .Select(p => p.Name)
                .ToListAsync();
        }

        public async Task<Platform?> FindByNameAsync(string name)
        {
            return await dbContext.Platforms.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name.Equals(name));
        }
    }
}