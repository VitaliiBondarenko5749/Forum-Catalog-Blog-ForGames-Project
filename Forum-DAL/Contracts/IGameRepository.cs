using Catalog_of_Games_DAL.Entities;

namespace Forum_DAL.Contracts
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        // Отримання колекції ігор, які зв'язані з відповідним постом(stored procedure).
        Task<IEnumerable<Game>> GetAllGamesForPostAsync(int postId);

        /// <summary>
        /// Знаходження гри за іменем 
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>id founded Game</returns>
        public Task<int> GetGameByNameAsync(string gameName);
    }
}
