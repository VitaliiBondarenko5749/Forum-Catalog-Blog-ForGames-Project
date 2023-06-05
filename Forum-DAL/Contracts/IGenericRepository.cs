namespace Forum_DAL.Contracts
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task ReplaceAsync(T model);
        Task<Guid> AddAsync(T model);
    }
}