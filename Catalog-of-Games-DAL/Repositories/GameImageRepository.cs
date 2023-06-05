using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;

namespace Catalog_of_Games_DAL.Repositories
{
    public class GameImageRepository : GenericRepository<GameImage>, IGameImageRepository
    {
        public GameImageRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }
    }
}