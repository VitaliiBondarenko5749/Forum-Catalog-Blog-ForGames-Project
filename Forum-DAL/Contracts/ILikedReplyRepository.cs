using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface ILikedReplyRepository : IGenericRepository<LikedReply>
    {
        // Отримання лайків для відповіді(stored procedure)
        Task<int> GetLikesForReplyAsync(Guid replyId);

        // Перевірка, чи поставив користувач лайк
        Task<bool> ExistAsync(LikedReply likedReply);

        // Видалення лайку
        Task DeleteByUserAndReplyIdAsync(LikedReply likedReply);
    }
}