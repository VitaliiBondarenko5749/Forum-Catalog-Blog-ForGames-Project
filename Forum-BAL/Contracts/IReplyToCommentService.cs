using Forum_BAL.DTO;
using Forum_DAL.Models;

namespace Forum_BAL.Contracts
{
    public interface IReplyToCommentService
    {
        // Додання відповіді на коментар
        Task AddReplyToCommentAsync(ReplyInsertDTO replyInsertDto);

        // Видалення відповіді з коментаря
        Task DeleteReplyFromCommentAsync(PostComment postComment, Guid replyId);
    }
}