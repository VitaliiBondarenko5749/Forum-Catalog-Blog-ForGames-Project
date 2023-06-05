using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }

        public async Task<List<Game>> GetGamesByCategoryAsync(string categoryName, int pageNumber, int pageSize)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return await dbContext.Games.AsNoTracking()
                .Where(g => g.GameCategories.Any(gc => gc.Category.Name.Equals(categoryName)))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public Task<Game> GetGameByNameAsync(string categoryName, string gameName)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8604 // Possible null reference argument.

            return dbContext.Games.AsNoTracking()
                .Include(g => g.GameCategories.Where(gc => gc.Category.Name.Equals(categoryName)))
                .ThenInclude(gc => gc.Category)
                .Include(g => g.Publisher)
                .Include(g => g.GameDevelopers)
                .ThenInclude(gd => gd.Developer)
                .Include(g => g.GameLanguages)
                .ThenInclude(gl => gl.Language)
                .Include(g => g.GamePlatforms)
                .ThenInclude(gp => gp.Platform)
                .FirstOrDefaultAsync(g => g.Name.Equals(gameName));

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task<List<Game>> FindByNameAsync(int pageNumber, int pageSize, string gameName)
        {
            return await dbContext.Games.AsNoTracking()
                .Where(g => g.Name.Contains(gameName))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(g => g.Name)
                .ToListAsync();
        }
    }
}