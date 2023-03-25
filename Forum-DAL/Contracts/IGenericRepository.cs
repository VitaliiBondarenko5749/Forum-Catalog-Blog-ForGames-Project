namespace Forum_DAL.Contracts
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task ReplaceAsync(T model);
        Task<int> AddAsync(T model); 
    }
}
