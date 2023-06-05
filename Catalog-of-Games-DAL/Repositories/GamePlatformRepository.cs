﻿using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;

namespace Catalog_of_Games_DAL.Repositories
{
    public class GamePlatformRepository : GenericRepository<GamePlatform>, IGamePlatformRepository
    {
        public GamePlatformRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }
    }
}