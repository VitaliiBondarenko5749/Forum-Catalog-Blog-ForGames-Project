using Catalog_of_Games_DAL.Data.Configurations;
using Catalog_of_Games_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Azure.Core.Pipeline;

namespace Catalog_of_Games_DAL.Data
{
    public class CatalogOfGamesContext : DbContext
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Publisher> Publishers => Set<Publisher>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<GameCategory> GamesCategories => Set<GameCategory>();
        public DbSet<Developer> Developers => Set<Developer>();
        public DbSet<GameDeveloper> GamesDevelopers => Set<GameDeveloper>();
        public DbSet<Platform> Platforms => Set<Platform>();
        public DbSet<GamePlatform> GamesPlatforms => Set<GamePlatform>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<GameLanguage> GamesLanguages => Set<GameLanguage>();
        public DbSet<GameComment> GamesComments => Set<GameComment>();
        public DbSet<LikedComment> LikedComments => Set<LikedComment>();
        public DbSet<GameImage> GamesImages => Set<GameImage>();
        public DbSet<Reply> Replies => Set<Reply>();
        public DbSet<LikedReply> LikedReplies => Set<LikedReply>();
        public DbSet<CommentReply> CommentsReplies => Set<CommentReply>();
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=GamesProjectDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new GameCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
            modelBuilder.ApplyConfiguration(new GameDeveloperConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformConfiguration());
            modelBuilder.ApplyConfiguration(new GamePlatformConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new GameLanguageConfiguration());
            modelBuilder.ApplyConfiguration(new GameCommentConfiguration());
            modelBuilder.ApplyConfiguration(new LikedCommentConfiguration());
            modelBuilder.ApplyConfiguration(new GameImageConfiguration());
            modelBuilder.ApplyConfiguration(new ReplyConfiguration());
            modelBuilder.ApplyConfiguration(new LikedReplyConfiguration());
            modelBuilder.ApplyConfiguration(new CommentReplyConfiguration());
        }

        public CatalogOfGamesContext(DbContextOptions<CatalogOfGamesContext> options) : base(options) { }
    }
}