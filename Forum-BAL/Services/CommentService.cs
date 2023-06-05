using FluentValidation.Results;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_BAL.Validators;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Text;

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
        public async Task AddCommentAsync(CommentInsertDTO commentInsertDto)
        {
            CommentValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(commentInsertDto);

            if (!result.IsValid)
            { 
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

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
                Id = Guid.NewGuid(),
                PostId = post.Id,
                CommentId = comment.Id
            };

            // Додаємо дані в проміжну таблицю
            await unitOfWork.PostCommentRepository.AddAsync(postComment);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }

        // Видалення коментаря з поста
        public async Task DeleteCommentAsync(PostComment postComment)
        {
            // Перевіряємо на пов'язаність коментар та пост + перевіряємо існування поста та коментаря
            await unitOfWork.PostCommentRepository.GetCommentIdByCommentAndPostIdsAsync(postComment);

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

        // Додаємо лайк до коментаря
        public async Task AddLikeAsync(PostComment postComment, LikedComment likedComment)
        {
            bool hasData = await unitOfWork.PostCommentRepository.ExistAsync(postComment);

            if (!hasData)
            {
                throw new InvalidDataException("There's no connected Post and Comment in the database.");
            }

            hasData = await unitOfWork.LikedCommentRepository.ExistAsync(likedComment);

            if (hasData)
            {
                throw new DuplicateWaitObjectException("There's the same data in the database.");
            }

            likedComment.Id = Guid.NewGuid();   
        
            await unitOfWork.LikedCommentRepository.AddAsync(likedComment);

            unitOfWork.Commit();
        }

        // Видаляємо лайк з коментаря
        public async Task DeleteLikeAsync(PostComment postComment, LikedComment likedComment)
        {
            bool hasData = await unitOfWork.PostCommentRepository.ExistAsync(postComment);

            if (!hasData)
            {
                throw new InvalidDataException("There's no connected Post and Comment in the database.");
            }

            await unitOfWork.LikedCommentRepository.DeleteByUserAndCommentIdAsync(likedComment);

            unitOfWork.Commit();
        }
    }
}