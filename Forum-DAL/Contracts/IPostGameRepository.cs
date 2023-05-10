using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface IPostGameRepository : IGenericRepository<PostGame>
    {
        // Отримуємо об'єкт класу PostGame, щоб знайти з'єднання поста та гри
        Task<PostGame> GetConnectedPostAndGameAsync(PostGame postGame);

        // Отримуємо колекцію всіх ігор, які пов'язані з постом
        Task<IEnumerable<Guid>> GetGamesIdAsync(Guid postId);

        // Видаляємо зв'язок з таблиці PostGame по GameId та PostId
        Task DeletePostGameAsync(PostGame postGame);
    }
}