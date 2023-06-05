using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace Forum_DAL.Repositories
{
    public class LikedReplyRepository : GenericRepository<LikedReply>, ILikedReplyRepository
    {
        public LikedReplyRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.LikedReplies") { }

        // Отримання лайків для відповіді(stored procedure)
        public async Task<int> GetLikesForReplyAsync(Guid replyId)
        {
            return await sqlConnection.ExecuteScalarAsync<int>("GetLikesForReply", param: new { ReplyId = replyId },
                transaction: dbTransaction, commandType: CommandType.StoredProcedure);
        }

        // Перевірка, чи поставив користувач лайк
        public async Task<bool> ExistAsync(LikedReply likedReply)
        {
            string query = "SELECT COUNT(*) FROM forum.LikedReplies WHERE UserId = @UserId, ReplyId = @ReplyId;";

            return await sqlConnection.QueryFirstOrDefaultAsync<int>(query, param: likedReply, transaction: dbTransaction) > 0;
        }

        // Видалення лайку
        public async Task DeleteByUserAndReplyIdAsync(LikedReply likedReply)
        {
            await sqlConnection.ExecuteAsync("DELETE FROM forum.LikedReplies WHERE UserId = @UserId, ReplyId = @ReplyId;",
                param: likedReply, transaction: dbTransaction);
        }
    }
}