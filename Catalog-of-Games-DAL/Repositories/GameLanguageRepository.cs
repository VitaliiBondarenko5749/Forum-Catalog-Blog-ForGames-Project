using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;

namespace Catalog_of_Games_DAL.Repositories
{
    public class GameLanguageRepository : GenericRepository<GameLanguage>, IGameLanguageRepository
    {
        public GameLanguageRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }
    }
}