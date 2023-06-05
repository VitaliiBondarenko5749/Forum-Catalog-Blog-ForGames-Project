using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }

        public async Task<List<string>> FindManyByNameAsync(string name)
        {
            return await dbContext.Developers.AsNoTracking()
                .Where(d => d.Name.Contains(name))
                .Select(d => d.Name)
                .ToListAsync();
        }

        public async Task<Developer?> FindByNameAsync(string name)
        {
            return await dbContext.Developers.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Name.Equals(name));
        }
    }
}