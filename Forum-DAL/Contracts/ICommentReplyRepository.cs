using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface ICommentReplyRepository : IGenericRepository<CommentReply>
    {
        // Отримання всіх ReplyId, які пов'язані з коментарем
        Task<IEnumerable<Guid>> GetRepliesIdAsync(Guid commentId);

        // Перевірка на зв'язаність коментаря та відповіді на коментар
        Task<bool> ExistAsync(CommentReply commentReply);
    }
}