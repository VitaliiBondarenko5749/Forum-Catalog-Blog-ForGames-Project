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
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.Comments") { }

        // Отримання коментарів для конкретного поста(stored procedure)
        public async Task<IEnumerable<Comment>> GetAllCommentsForPostAsync(int postId)
        {
            return await sqlConnection.QueryAsync<Comment>("GetAllCommentsForPost", param: new { PostId = postId },
                transaction: dbTransaction, commandType: CommandType.StoredProcedure);
        }
    }
}
