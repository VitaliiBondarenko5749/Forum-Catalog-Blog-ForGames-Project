using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Repositories
{
    public class ReplyRepository : GenericRepository<Reply>, IReplyRepository
    {
        public ReplyRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.Replies") { }

        // Отримання всіх відповідей на коментар(stored procedure)
        public async Task<IEnumerable<Reply>> GetAllRepliesForCommentAsync(int commentId)
        {
            return await sqlConnection.QueryAsync<Reply>("GetAllRepliesForComment", param: new {  CommentId = commentId },
                transaction: dbTransaction, commandType: CommandType.StoredProcedure);
        }
    }
}
