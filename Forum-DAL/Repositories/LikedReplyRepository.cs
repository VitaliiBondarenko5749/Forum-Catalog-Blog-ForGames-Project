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
    }
}