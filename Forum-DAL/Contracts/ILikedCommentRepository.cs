using Forum_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum_DAL.Contracts
{
    public interface ILikedCommentRepository : IGenericRepository<LikedComment>
    {
        // Отримання кількості лайків для конкретного коментаря(stored procedure)
        Task<int> GetLikesForCommentAsync(int commentId);
    }
}
