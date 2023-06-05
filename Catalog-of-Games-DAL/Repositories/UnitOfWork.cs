using Catalog_of_Games_DAL.Data;
using Catalog_of_Games_DAL.Repositories.Contracts;

namespace Catalog_of_Games_DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly CatalogOfGamesContext dbContext;

        public  UnitOfWork(CatalogOfGamesContext dbContext, ICategoryRepository categoryRepository, IGameRepository gameRepository,
            IDeveloperRepository developerRepository, ILanguageRepository languageRepository, IPlatformRepository platformRepository,
            IPublisherRepository publishertRepository, IGameCommentRepository gameCommentRepository,
            IGameDeveloperRepository gameDeveloperRepository, IGameLanguageRepository gameLanguageRepository,
            IGamePlatformRepository gamePlatformRepository, ICommentReplyRepository commentReplyRepository,
            IGameImageRepository gameImageRepository, ILikedCommentRepository likedCommentRepository,
            ILikedReplyRepository likedReplyRepository, IReplyRepository replyRepository)
        {
            this.dbContext = dbContext;

            CategoryRepository = categoryRepository;
            GameRepository = gameRepository;
            DeveloperRepository = developerRepository;
            LanguageRepository = languageRepository;
            PlatformRepository = platformRepository;
            PublisherRepository = publishertRepository;
            GameCommentRepository = gameCommentRepository;
            GameDeveloperRepository = gameDeveloperRepository;
            GameLanguageRepository = gameLanguageRepository;
            GamePlatformRepository = gamePlatformRepository;
            CommentReplyRepository = commentReplyRepository;
            GameImageRepository = gameImageRepository;
            LikedCommentRepository = likedCommentRepository;
            LikedReplyRepository = likedReplyRepository;
            ReplyRepository = replyRepository;
        }

        public ICategoryRepository CategoryRepository { get; }
        public IGameRepository GameRepository { get; }
        public IDeveloperRepository DeveloperRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public IPlatformRepository PlatformRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        public IGameCommentRepository GameCommentRepository { get; }
        public IGameDeveloperRepository GameDeveloperRepository { get; }
        public IGameLanguageRepository GameLanguageRepository { get; }
        public IGamePlatformRepository GamePlatformRepository { get; }
        public ICommentReplyRepository CommentReplyRepository { get; }
        public IGameImageRepository GameImageRepository { get; }
        public ILikedCommentRepository LikedCommentRepository { get; }
        public ILikedReplyRepository LikedReplyRepository { get; }
        public IReplyRepository ReplyRepository { get; }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}