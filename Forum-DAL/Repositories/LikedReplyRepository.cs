using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Repositories
{
    public class LikedReplyRepository : GenericRepository<LikedReply>, ILikedReplyRepository
    {
        public LikedReplyRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.LikedReplies") { }

        // Отримання лайків для відповіді(stored procedure)
        public async Task<int> GetLikesForReplyAsync(int replyId)
        {
            return await sqlConnection.ExecuteScalarAsync<int>("GetLikesForReply", param: new { ReplyId = replyId },
                transaction: dbTransaction, commandType: CommandType.StoredProcedure);
        }
    }
}
