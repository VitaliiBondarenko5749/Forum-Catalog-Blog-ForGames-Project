using Forum_BAL.DTO;
using Forum_DAL.Models;

namespace Forum_BAL.Contracts
{
    public interface IPostService
    {
        // Отримуємо всі пости та ігри, які пов'язані до кожного поста
        Task<IEnumerable<ShortPostInfoDTO>> GetAllPostsAndGamesAsync();

        /* Отримуємо інформацію про конкретний пост та всю інформацію про нього:
         * Коментарі
         * Відповіді на коментар */
        Task<ConcretePostInfoDTO> GetPostAsync(int postId);

        // Вставляємо новий пост
        Task AddPostAsync(PostInsertUpdateDTO postInsertDto);

        // Оновлюємо пост
        Task UpdatePostAsync(PostInsertUpdateDTO postUpdateDto);

        // Видаляємо пост
        Task DeletePostAsync(int postId);
    }
}
