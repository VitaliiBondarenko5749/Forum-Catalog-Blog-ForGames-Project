﻿using Dapper;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Data;
using System.Data.SqlClient;

namespace Forum_DAL.Repositories
{
    public class CommentReplyRepository : GenericRepository<CommentReply>, ICommentReplyRepository
    {
        public CommentReplyRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "forum.CommentsReplies") { }

        // Отримання всіх ReplyId, які пов'язані з коментарем
        public async Task<IEnumerable<Guid>> GetRepliesIdAsync(Guid commentId)
        {
            string sqlQuery = "SELECT ReplyId FROM forum.CommentsReplies WHERE CommentId = @CommentId;";

            return await sqlConnection.QueryAsync<Guid>(sqlQuery, param: new { CommentId = commentId },
                transaction: dbTransaction);
        }
    }
}