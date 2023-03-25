using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Contracts
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        // Отримання коментарів для конкретного поста(stored procedure)
        Task<IEnumerable<Comment>> GetAllCommentsForPostAsync(int postId);
    }
}
