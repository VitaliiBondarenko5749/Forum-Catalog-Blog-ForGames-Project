using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Forum_DAL.Repositories
{
    public class PostGameRepository : GenericRepository<PostGame>, IPostGameRepository
    {
        public PostGameRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.PostsGames") { }

        // Отримуємо об'єкт класу PostGame, щоб знайти з'єднання поста та гри
        public async Task<PostGame> GetConnectedPostAndGameAsync(PostGame postGame)
        {
            string sqlQuery = "SELECT TOP 1 PostId, GameId FROM forum.PostsGames" +
                " WHERE PostId = @PostId AND GameId = @GameId;";

            return await sqlConnection.QueryFirstAsync<PostGame>(sqlQuery, param: postGame, transaction: dbTransaction);
        }

        // Отримуємо колекцію всіх ігор, які пов'язані з постом
        public async Task<IEnumerable<int>> GetGamesIdAsync(int postId)
        {
            string sqlQuery = "SELECT GameId FROM forum.PostsGames WHERE PostId = @PostId;";

            return await sqlConnection.QueryAsync<int>(sqlQuery, param: new { PostId = postId },
                transaction: dbTransaction);
        }

        // Видаляємо зв'язок з таблиці PostGame по GameId та PostId
        public async Task DeletePostGameAsync(PostGame postGame)
        {
            string sqlQuery = "DELETE FROM forum.PostsGames WHERE PostId = @PostId AND GameId = @GameId;";

            await sqlConnection.QueryAsync(sqlQuery, param: postGame, transaction: dbTransaction);
        }
    }
}
