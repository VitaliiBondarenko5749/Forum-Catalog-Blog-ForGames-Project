using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Contracts
{
    public interface IPostCommentRepository : IGenericRepository<PostComment>
    {
        // Отримання всіх CommentId, які пов'язані з постом
        Task<IEnumerable<int>> GetCommentsIdAsync(int postId);
    }
}
