using FluentValidation.Results;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_BAL.Validators;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Text;

namespace Forum_BAL.Services
{
    public class ReplyService : IReplyService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReplyService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Додання відповіді на коментар
        public async Task AddReplyAsync(ReplyInsertDTO replyInsertDto)
        {
            ReplyValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(replyInsertDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            // Перевіряємо на пов'язаність коментар та пост + перевіряємо існування поста та коментаря
            await unitOfWork.PostCommentRepository
                .GetCommentIdByCommentAndPostIdsAsync(new PostComment
                { PostId = replyInsertDto.PostId, CommentId = replyInsertDto.CommentId });

            Reply reply = new()
            {
                Id = Guid.NewGuid(),
                Content = replyInsertDto.Content,
                WhenReplied = DateTime.Now,
            };

            // Додаємо відповідь на коментар
            await unitOfWork.ReplyRepository.AddAsync(reply);

            CommentReply commentReply = new()
            {
                Id = Guid.NewGuid(),
                CommentId = replyInsertDto.CommentId,
                ReplyId = reply.Id
            };

            // Вставляємо дані в проміжну таблицю
            await unitOfWork.CommentReplyRepository.AddAsync(commentReply);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }

        // Видалення відповіді з коментаря
        public async Task DeleteReplyAsync(PostComment postComment, Guid replyId)
        {
            // Перевіряємо на пов'язаність коментар та пост + перевіряємо існування поста та коментаря
            await unitOfWork.PostCommentRepository.GetCommentIdByCommentAndPostIdsAsync(postComment);

            // Видаляємо відповідь на коментар(з каскадним видаленням у нас видаляться лайки, пов'язаність з коментарем)
            await unitOfWork.ReplyRepository.DeleteAsync(replyId);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }

        // Додаємо лайк до коментаря
        public async Task AddLikeAsync(PostComment postComment, LikedReply likedReply)
        {
            bool hasData = await unitOfWork.PostCommentRepository.ExistAsync(postComment);

            if (!hasData)
            {
                throw new InvalidDataException("There's no connected Post and Comment in the database.");
            }

            CommentReply commentReply = new() { CommentId = postComment.CommentId, ReplyId = postComment.CommentId };

            hasData = await unitOfWork.CommentReplyRepository.ExistAsync(commentReply);

            if (!hasData)
            {
                throw new InvalidDataException("There's no connected Comment and Reply in the database.");
            }

            hasData = await unitOfWork.LikedReplyRepository.ExistAsync(likedReply);

            if (hasData)
            {
                throw new DuplicateWaitObjectException("There's the same data in the database.");
            }

            await unitOfWork.LikedReplyRepository.AddAsync(likedReply);

            unitOfWork.Commit();
        }

        // Видаляємо лайк з коментаря
        public async Task DeleteLikeAsync(PostComment postComment, LikedReply likedReply)
        {
            bool hasData = await unitOfWork.PostCommentRepository.ExistAsync(postComment);

            if (!hasData)
            {
                throw new InvalidDataException("There's no connected Post and Comment in the database.");
            }

            CommentReply commentReply = new() { CommentId = postComment.CommentId, ReplyId = likedReply.ReplyId };

            hasData = await unitOfWork.CommentReplyRepository.ExistAsync(commentReply);

            if (!hasData)
            {
                throw new InvalidDataException("There's no connected Comment and Reply in the database.");
            }

            await unitOfWork.LikedReplyRepository.DeleteByUserAndReplyIdAsync(likedReply);

            unitOfWork.Commit();
        }
    }
}