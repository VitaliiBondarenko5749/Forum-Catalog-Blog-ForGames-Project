using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Catalog_of_Games_DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CatalogOfGamesContext dbContext;
        private readonly DbSet<T> table;

        public GenericRepository(CatalogOfGamesContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await table.AddAsync(entity);

            return entity;
        }

        public async Task<List<T>> GetAllAsync(int pageNumber = 0, int pageSize = 0)
        {
            if(pageNumber.Equals(0) && pageSize.Equals(0))
            {
                return await table.ToListAsync();
            }

            return await table.OrderBy(t => t.GetHashCode())
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await table.FindAsync(id) 
                ?? throw new NullReferenceException("Entity type is nullable!");
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Update(entity);

            await Task.Delay(1000);
        }

        public async Task DeleteAsync(Guid id)
        {
            T? entity = await table.FindAsync(id) 
                ?? throw new NullReferenceException($"Could not find Entity by id: {id}");

            table.Remove(entity);
        }
    }
}