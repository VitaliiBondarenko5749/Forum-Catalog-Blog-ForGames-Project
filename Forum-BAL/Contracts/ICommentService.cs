using Forum_BAL.DTO;
using Forum_DAL.Models;

namespace Forum_BAL.Contracts
{
    public interface ICommentService
    {
        // Додання нового коментаря до поста
        Task AddCommentToPostAsync(CommentInsertDTO commentInsertDto);

        // Видалення коментаря з поста
        Task DeleteCommentFromPostAsync(PostComment postComment);
    }
}