using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface ILikedReplyRepository : IGenericRepository<LikedReply>
    {
        // Отримання лайків для відповіді(stored procedure)
        Task<int> GetLikesForReplyAsync(int replyId);
    }
}
