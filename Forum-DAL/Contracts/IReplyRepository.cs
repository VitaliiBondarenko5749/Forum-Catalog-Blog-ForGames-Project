using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Contracts
{
    public interface IReplyRepository : IGenericRepository<Reply>
    {
        // Отримання всіх відповідей на коментар(stored procedure)
        Task<IEnumerable<Reply>> GetAllRepliesForCommentAsync(int commentId);
    }
}
