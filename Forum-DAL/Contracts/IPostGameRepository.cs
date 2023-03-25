using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Contracts
{
    public interface IPostGameRepository : IGenericRepository<PostGame>
    {
        // Отримуємо об'єкт класу PostGame, щоб знайти з'єднання поста та гри
        Task<PostGame> GetConnectedPostAndGameAsync(PostGame postGame);

        // Отримуємо колекцію всіх ігор, які пов'язані з постом
        Task<IEnumerable<int>> GetGamesIdAsync(int postId);

        // Видаляємо зв'язок з таблиці PostGame по GameId та PostId
        Task DeletePostGameAsync(PostGame postGame);
    }
}
