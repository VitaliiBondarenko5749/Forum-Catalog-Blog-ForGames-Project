﻿using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace Forum_DAL.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "gamecatalog.Games") { }

        // Отримання колекції ігор, які зв'язані з відповідним постом(stored procedure).
        public async Task<IEnumerable<Game>> GetAllGamesForPostAsync(Guid postId)
        {
            return await sqlConnection.QueryAsync<Game>("GetAllGamesForPost", param: new { PostId = postId },
                transaction: dbTransaction, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Знаходження гри за іменем 
        /// </summary>
        /// <param name="gameName"></param>
        /// <returns>id founded Game</returns>
        public async Task<Guid> GetGameIdByNameAsync(string gameName)
        {
            string sqlQuery = "SELECT TOP 1 Id FROM gamecatalog.Games WHERE Name = @Name;";

            return await sqlConnection.QueryFirstAsync<Guid>(sqlQuery, param: new { Name = gameName },
                transaction: dbTransaction);
        }
    }
}