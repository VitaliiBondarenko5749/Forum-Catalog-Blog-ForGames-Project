using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        // Отримання коментарів для конкретного поста(stored procedure)
        Task<IEnumerable<Comment>> GetAllCommentsForPostAsync(Guid postId);
    }
}