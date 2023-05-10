using Forum_DAL.Contracts;
using System.Data;

namespace Forum_DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbTransaction transaction;

        public UnitOfWork(IPostRepository postRepository, IGameRepository gameRepository,
            ICommentRepository commentRepository, ILikedCommentRepository likedCommentRepository,
            IReplyRepository replyRepository, ILikedReplyRepository likedReplyRepository,
            IPostGameRepository postGameRepository, IPostCommentRepository postCommentRepository,
            ICommentReplyRepository commentReplyRepository, IDbTransaction transaction)
        {
            PostRepository = postRepository;
            GameRepository = gameRepository;
            CommentRepository = commentRepository;
            LikedCommentRepository = likedCommentRepository;
            ReplyRepository = replyRepository;
            LikedReplyRepository = likedReplyRepository;
            PostGameRepository = postGameRepository;
            PostCommentRepository = postCommentRepository;
            CommentReplyRepository = commentReplyRepository;
            this.transaction = transaction;
        }

        public IPostRepository PostRepository { get; }
        public IGameRepository GameRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ILikedCommentRepository LikedCommentRepository { get; }
        public IReplyRepository ReplyRepository { get; }
        public ILikedReplyRepository LikedReplyRepository { get; }
        public IPostGameRepository PostGameRepository { get; }
        public IPostCommentRepository PostCommentRepository { get; }
        public ICommentReplyRepository CommentReplyRepository { get; }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            transaction.Connection?.Close();
            transaction.Connection?.Dispose();
            transaction.Dispose();
        }
    }
}