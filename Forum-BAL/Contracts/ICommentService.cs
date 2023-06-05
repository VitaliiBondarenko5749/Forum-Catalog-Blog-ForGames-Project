using Forum_BAL.DTO;
using Forum_DAL.Models;

namespace Forum_BAL.Contracts
{
    public interface ICommentService
    {
        // Додання нового коментаря до поста
        Task AddCommentAsync(CommentInsertDTO commentInsertDto);

        // Видалення коментаря з поста
        Task DeleteCommentAsync(PostComment postComment);

        // Додаємо лайк до коментаря
        Task AddLikeAsync(PostComment postComment, LikedComment likedComment);

        // Видаляємо лайк з коментаря
        Task DeleteLikeAsync(PostComment postComment, LikedComment likedComment);
    }
}