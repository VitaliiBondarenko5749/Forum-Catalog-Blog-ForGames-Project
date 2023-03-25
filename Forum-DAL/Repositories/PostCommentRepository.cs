using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace Forum_DAL.Repositories
{
    public class PostCommentRepository : GenericRepository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.PostsComments") { }

        // Отримання всіх CommentId, які пов'язані з постом
        public async Task<IEnumerable<int>> GetCommentsIdAsync(int postId)
        {
            string sqlQuery = "SELECT CommentId FROM forum.PostsComments WHERE PostId = @PostId;";

            return await sqlConnection.QueryAsync<int>(sqlQuery, param: new { PostId = postId },
                transaction: dbTransaction);
        }
    }
}
