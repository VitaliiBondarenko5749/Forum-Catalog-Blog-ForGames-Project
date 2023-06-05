using Forum_BAL.DTO;
using Forum_DAL.Models;

namespace Forum_BAL.Contracts
{
    public interface IReplyService
    {
        // Додання відповіді на коментар
        Task AddReplyAsync(ReplyInsertDTO replyInsertDto);

        // Видалення відповіді з коментаря
        Task DeleteReplyAsync(PostComment postComment, Guid replyId);

        // Додаємо лайк до коментаря
        Task AddLikeAsync(PostComment postComment, LikedReply likedReply);

        // Видаляємо лайк з коментаря
        Task DeleteLikeAsync(PostComment postComment, LikedReply likedReply);
    }
}