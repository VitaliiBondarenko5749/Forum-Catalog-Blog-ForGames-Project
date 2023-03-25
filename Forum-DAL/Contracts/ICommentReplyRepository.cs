using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Contracts
{
    public interface ICommentReplyRepository : IGenericRepository<CommentReply>
    {
        // Отримання всіх ReplyId, які пов'язані з коментарем
        Task<IEnumerable<int>> GetRepliesIdAsync(int commentId);
    }
}
