namespace Forum_DAL.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }
        IGameRepository GameRepository { get; }
        ICommentRepository CommentRepository { get; }
        ILikedCommentRepository LikedCommentRepository { get; }
        IReplyRepository ReplyRepository { get; }
        ILikedReplyRepository LikedReplyRepository { get; }
        IPostGameRepository PostGameRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        ICommentReplyRepository CommentReplyRepository { get; }
        void Commit();
        new void Dispose();
    }
}