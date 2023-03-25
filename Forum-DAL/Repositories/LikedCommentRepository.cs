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
    public class LikedCommentRepository : GenericRepository<LikedComment>, ILikedCommentRepository
    {
        public LikedCommentRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
         : base(sqlConnection, dbTransaction, "forum.LikedComments") { }

        // Отримання кількості лайків для конкретного коментаря(stored procedure)
        public async Task<int> GetLikesForCommentAsync(int commentId)
        {
            return await sqlConnection.ExecuteScalarAsync<int>("GetLikesForComment", param: new { CommentId = commentId },
                transaction: dbTransaction, commandType: CommandType.StoredProcedure);
        }
    }
}
