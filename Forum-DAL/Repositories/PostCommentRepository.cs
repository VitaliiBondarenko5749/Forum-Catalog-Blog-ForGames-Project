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
        public async Task<IEnumerable<Guid>> GetCommentsIdAsync(Guid postId)
        {
            string sqlQuery = "SELECT CommentId FROM forum.PostsComments WHERE PostId = @PostId;";

            return await sqlConnection.QueryAsync<Guid>(sqlQuery, param: new { PostId = postId },
                transaction: dbTransaction);
        }

        // Отримання значення CommentId з таблиці PostsComments, для того щоб перевірити коментар та пост на зв'язаність
        public async Task<Guid> GetCommentIdByCommentAndPostIdsAsync(PostComment postComment)
        {
            string sqlQuery = "SELECT TOP 1 CommentId FROM forum.PostsComments WHERE PostId = @PostId AND CommentId = @CommentId;";

            return await sqlConnection.QueryFirstAsync<Guid>(sqlQuery, param: postComment,
                transaction: dbTransaction);
        }
    }
}