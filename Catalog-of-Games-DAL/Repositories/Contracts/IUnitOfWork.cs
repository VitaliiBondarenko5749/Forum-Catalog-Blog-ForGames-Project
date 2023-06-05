namespace Catalog_of_Games_DAL.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IGameRepository GameRepository { get; }
        IDeveloperRepository DeveloperRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IPlatformRepository PlatformRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        IGameCommentRepository GameCommentRepository { get; }
        IGameDeveloperRepository GameDeveloperRepository { get; }
        IGameLanguageRepository GameLanguageRepository { get; }
        IGamePlatformRepository GamePlatformRepository { get; }
        ICommentReplyRepository CommentReplyRepository { get; }
        IGameImageRepository GameImageRepository { get; }
        ILikedCommentRepository LikedCommentRepository { get; }
        ILikedReplyRepository LikedReplyRepository { get; }
        IReplyRepository ReplyRepository { get; }

        Task SaveChangesAsync();
    }
}