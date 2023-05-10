namespace Forum_DAL.Contracts
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task ReplaceAsync(T model);
        Task<Guid> AddAsync(T model); 
    }
}