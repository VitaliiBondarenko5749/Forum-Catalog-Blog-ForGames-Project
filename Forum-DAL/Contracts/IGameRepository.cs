using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        // Отримання колекції ігор, які зв'язані з відповідним постом(stored procedure).
        Task<IEnumerable<Game>> GetAllGamesForPostAsync(Guid postId);

        /// <summary>
        /// Знаходження гри за іменем 
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>id founded Game</returns>
        public Task<Guid> GetGameIdByNameAsync(string gameName);
    }
}