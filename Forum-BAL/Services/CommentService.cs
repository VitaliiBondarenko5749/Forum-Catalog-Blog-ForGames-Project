using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_DAL.Contracts;
using Forum_DAL.Models;

namespace Forum_BAL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Додання нового коментаря до поста
        public async Task AddCommentToPostAsync(CommentInsertDTO commentInsertDto)
        {
            // Знайдемо пост в базі даних для перевірки його існування
            Post post = await unitOfWork.PostRepository.GetAsync(commentInsertDto.PostId);

            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                Content = commentInsertDto.Content,
                WhenReplied = DateTime.Now, 
            };

            // Додаємо новий коментар до бази даних
            await unitOfWork.CommentRepository.AddAsync(comment);

            PostComment postComment = new()
            {
                PostId = post.Id,
                CommentId = comment.Id
            };

            // Додаємо дані в проміжну таблицю
            await unitOfWork.PostCommentRepository.AddAsync(postComment);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }

        // Видалення коментаря з поста
        public async Task DeleteCommentFromPostAsync(PostComment postComment)
        {
            // Перевіряємо на пов'язаність коментар та пост + перевіряємо існування поста та коментаря
            _ = await unitOfWork.PostCommentRepository.GetCommentIdByCommentAndPostIdsAsync(postComment);

            // Отримуємо всі ReplyId, які пов'язані з коментарем
            IEnumerable<Guid>? repliesId = await unitOfWork.CommentReplyRepository.GetRepliesIdAsync(postComment.CommentId);

            // Проходимо ітерацію по отриманих repliesId, щоб видалити всю інформацію про них в базі даних
            if(repliesId != null)
            {
                foreach(Guid replyId in repliesId)
                {
                    // Видаляємо відповідь на коментар(з каскадним видаленням у нас видаляться лайки, пов'язаність з коментарем)
                    await unitOfWork.ReplyRepository.DeleteAsync(replyId);
                }
            }

            // Видаляємо відповідь(з каскадним видаленням у нас видаляться лайки, пов'язаність з постом)
            await unitOfWork.CommentRepository.DeleteAsync(postComment.CommentId);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }
    }
}