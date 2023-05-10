using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface IReplyRepository : IGenericRepository<Reply>
    {
        // Отримання всіх відповідей на коментар(stored procedure)
        Task<IEnumerable<Reply>> GetAllRepliesForCommentAsync(Guid commentId);
    }
}