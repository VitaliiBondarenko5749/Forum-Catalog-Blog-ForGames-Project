using Forum_DAL.Models;

namespace Forum_DAL.Contracts
{
    public interface IPostCommentRepository : IGenericRepository<PostComment>
    {
        // Отримання всіх CommentId, які пов'язані з постом
        Task<IEnumerable<Guid>> GetCommentsIdAsync(Guid postId);

        // Отримання значення CommentId з таблиці PostsComments, для того щоб перевірити коментар та пост на зв'язаність
        Task<Guid> GetCommentIdByCommentAndPostIdsAsync(PostComment postComment);

        // Перевірка на зв'язаність поста та коментаря
        Task<bool> ExistAsync(PostComment postComment);
    }
}