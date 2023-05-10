using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_DAL.Contracts;
using Forum_DAL.Models;

namespace Forum_BAL.Services
{
    public class ReplyToCommentService : IReplyToCommentService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReplyToCommentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Додання відповіді на коментар
        public async Task AddReplyToCommentAsync(ReplyInsertDTO replyInsertDto)
        {
            // Перевіряємо на пов'язаність коментар та пост + перевіряємо існування поста та коментаря
            _ = await unitOfWork.PostCommentRepository
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
                CommentId = replyInsertDto.CommentId,
                ReplyId = reply.Id
            };

            // Вставляємо дані в проміжну таблицю
            await unitOfWork.CommentReplyRepository.AddAsync(commentReply);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }

        // Видалення відповіді з коментаря
        public async Task DeleteReplyFromCommentAsync(PostComment postComment, Guid replyId)
        {
            // Перевіряємо на пов'язаність коментар та пост + перевіряємо існування поста та коментаря
            _ = await unitOfWork.PostCommentRepository.GetCommentIdByCommentAndPostIdsAsync(postComment);

            // Видаляємо відповідь на коментар(з каскадним видаленням у нас видаляться лайки, пов'язаність з коментарем)
            await unitOfWork.ReplyRepository.DeleteAsync(replyId);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }
    }
}