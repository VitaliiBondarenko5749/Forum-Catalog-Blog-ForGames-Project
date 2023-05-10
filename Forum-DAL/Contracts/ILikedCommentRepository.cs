using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface ILikedCommentRepository : IGenericRepository<LikedComment>
    {
        // Отримання кількості лайків для конкретного коментаря(stored procedure)
        Task<int> GetLikesForCommentAsync(Guid commentId);
    }
}