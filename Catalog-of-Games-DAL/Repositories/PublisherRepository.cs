using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(CatalogOfGamesContext dbContext) : base(dbContext) { }

        public async Task<Publisher?> FindByNameAsync(string name)
        {
            return await dbContext.Publishers.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name.Equals(name));
        }

        public async Task DeleteByNameAsync(string name)
        {
            Publisher publisher = await dbContext.Publishers.FirstOrDefaultAsync(p => p.Name.Equals(name))
                ?? throw new InvalidDataException($"There's no publisher with name: {name}");

            dbContext.Publishers.Remove(publisher);
        }
    }
}