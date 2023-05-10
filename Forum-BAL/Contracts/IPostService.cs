using Forum_BAL.DTO;

namespace Forum_BAL.Contracts
{
    public interface IPostService
    {
        // Отримуємо всі пости та ігри, які пов'язані до кожного поста
        Task<IEnumerable<ShortPostInfoDTO>> GetAllPostsAsync();

        /* Отримуємо інформацію про конкретний пост та всю інформацію про нього:
         * Коментарі
         * Відповіді на коментар */
        Task<ConcretePostInfoDTO> GetPostAsync(Guid postId);

        // Вставляємо новий пост
        Task AddPostAsync(PostInsertUpdateDTO postInsertDto);

        // Оновлюємо пост
        Task UpdatePostAsync(PostInsertUpdateDTO postUpdateDto);

        // Видаляємо пост
        Task DeletePostAsync(Guid postId);
    }
}