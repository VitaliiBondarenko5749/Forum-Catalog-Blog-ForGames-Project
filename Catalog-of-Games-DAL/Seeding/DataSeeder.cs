using Bogus;
using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_DAL.Seeding
{
    public static class DataSeeder
    {
        public static List<Game> Games { get; private set; } = null!;
        public static List<Publisher> Publishers { get; private set; } = null!;
        public static List<Platform> Platforms { get; private set; } = null!;
        public static List<GamePlatform> GamesPlatforms { get; private set; } = null!;
        public static List<Language> Languages { get; private set; } = null!;
        public static List<GameLanguage> GamesLanguages { get; private set; } = null!;
        public static List<Developer> Developers { get; private set; } = null!;
        public static List<GameDeveloper> GamesDevelopers { get; private set; } = null!;
        public static List<Category> Categories { get; private set; } = null!;
        public static List<GameCategory> GamesCategories { get; private set; } = null!;
        public static List<GameComment> GamesComments { get; private set; } = null!;
        public static List<CommentReply> CommentsReplies { get; private set; } = null!;
        public static List<GameImage> GamesImages { get; private set; } = null!;
        public static List<LikedComment> LikedComments { get; private set; } = null!;
        public static List<LikedReply> LikedReplies { get; private set; } = null!;
        public static List<Reply> Replies { get; private set; } = null!;

        static DataSeeder()
        {
            if (Publishers is null && Games is null && Platforms is null && GamesPlatforms is null && Languages is null
                && GamesLanguages is null && Developers is null && GamesDevelopers is null && CommentsReplies is null
                && GamesImages is null && GamesImages is null && LikedComments is null && LikedReplies is null && Replies is null)
            {
                Initialize();
            }
        }

        private static void Initialize()
        {
            Publishers = GeneratePublisher().Generate(10);
            Games = GenerateGame().Generate(20);
            Platforms = GeneratePlatform().Generate(10);
            Languages = GenerateLanguage().Generate(10);
            Developers = GenerateDeveloper().Generate(10);
            Categories = GenerateCategory().Generate(10);

            GamesPlatforms = GenerateGamePlatform().Generate(20);
            GamesLanguages = GenerateGameLanguage().Generate(20);
            GamesDevelopers = GenerateGameDeveloper().Generate(20);
            GamesCategories = GenerateGameCategory().Generate(20);
            GamesComments = GenerateGameComment().Generate(20);

            List<GamePlatform> uniqueGamesPlatforms = GamesPlatforms.GroupBy(gp => new { gp.GameId, gp.PlatformId })
                .Select(g => g.First())
                .ToList();

            GamesPlatforms = uniqueGamesPlatforms;

            List<GameLanguage> uniqueGamesLanguages = GamesLanguages.GroupBy(gl => new { gl.GameId, gl.LanguageId })
                .Select(g => g.First())
                .ToList();

            GamesLanguages = uniqueGamesLanguages;

            List<GameDeveloper> uniqueGamesDevelopers = GamesDevelopers.GroupBy(gd => new { gd.GameId, gd.DeveloperId })
                .Select(g => g.First())
                .ToList();

            GamesDevelopers = uniqueGamesDevelopers;

            List<GameCategory> uniqueGamesCategories = GamesCategories.GroupBy(gc => new { gc.GameId, gc.CategoryId })
                .Select(g => g.First())
                .ToList();

            GamesDevelopers = uniqueGamesDevelopers;
            GamesImages = GenerateGameImage().Generate(5);
            LikedComments = GenerateLikedComment().Generate(20);
            Replies = GenerateReply().Generate(20);
            LikedReplies = GenerateLikedReply().Generate(20);   
            CommentsReplies = GenerateCommentReply().Generate(15);

            List<CommentReply> uniqueCommentsReplies = CommentsReplies.GroupBy(cr => new { cr.CommentId, cr.Reply })
                .Select(g => g.First())
                .ToList();

            CommentsReplies = uniqueCommentsReplies;
        }

        private static Faker<Publisher> GeneratePublisher()
        {
            return new Faker<Publisher>().RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Company.CompanyName());
        }

        private static Faker<Game> GenerateGame()
        {
            return new Faker<Game>().RuleFor(g => g.Id, f => Guid.NewGuid())
            .RuleFor(g => g.Name, f => f.Commerce.ProductName())
            .RuleFor(g => g.ReleaseDate, f => f.Date.Past(5))
            .RuleFor(g => g.PublisherId, f => f.PickRandom(Publishers).Id)
            .RuleFor(g => g.Rating, f => f.Random.Float(1, 10))
            .RuleFor(g => g.Description, f => f.Lorem.Paragraph())
            .RuleFor(g => g.MainImage, f => f.Image.PicsumUrl());
        }

        private static Faker<Platform> GeneratePlatform()
        {
            return new Faker<Platform>().RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Company.CompanyName());
        }

        private static Faker<GamePlatform> GenerateGamePlatform()
        {
            return new Faker<GamePlatform>().RuleFor(gp => gp.Id, f => Guid.NewGuid())
                .RuleFor(gp => gp.GameId, f => f.PickRandom(Games).Id)
                .RuleFor(gp => gp.PlatformId, f => f.PickRandom(Platforms).Id);
        }

        private static Faker<Language> GenerateLanguage()
        {
            return new Faker<Language>().RuleFor(l => l.Id, f => Guid.NewGuid())
                .RuleFor(l => l.Name, f => f.Name.Suffix());
        }

        private static Faker<GameLanguage> GenerateGameLanguage()
        {
            return new Faker<GameLanguage>().RuleFor(gl => gl.Id, f => Guid.NewGuid())
                .RuleFor(gl => gl.GameId, f => f.PickRandom(Games).Id)
                .RuleFor(gl => gl.LanguageId, f => f.PickRandom(Languages).Id);
        }

        private static Faker<Developer> GenerateDeveloper()
        {
            return new Faker<Developer>().RuleFor(d => d.Id, f => Guid.NewGuid())
                .RuleFor(d => d.Name, f => f.Company.CompanyName());
        }

        private static Faker<GameDeveloper> GenerateGameDeveloper()
        {
            return new Faker<GameDeveloper>().RuleFor(gp => gp.Id, f => Guid.NewGuid())
                .RuleFor(gd => gd.GameId, f => f.PickRandom(Games).Id)
                .RuleFor(gd => gd.DeveloperId, f => f.PickRandom(Developers).Id);
        }

        private static Faker<Category> GenerateCategory()
        {
            return new Faker<Category>().RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Lorem.Word())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.Icon, f => f.Internet.Url());
        }

        private static Faker<GameCategory> GenerateGameCategory()
        {
            return new Faker<GameCategory>().RuleFor(gc => gc.Id, f => Guid.NewGuid())
                .RuleFor(gc => gc.GameId, f => f.PickRandom(Games).Id)
                .RuleFor(gc => gc.CategoryId, f => f.PickRandom(Categories).Id);
        }

        private static Faker<GameComment> GenerateGameComment()
        {
            return new Faker<GameComment>().RuleFor(gc => gc.Id, f => Guid.NewGuid())
                .RuleFor(gc => gc.UserId, f => Guid.NewGuid())
                .RuleFor(gc => gc.GameId, f => f.PickRandom(Games).Id)
                .RuleFor(gc => gc.Content, f => f.Lorem.Word())
                .RuleFor(gc => gc.CreatedAt, f => DateTime.Now);
        }

        private static Faker<Reply> GenerateReply()
        {
            return new Faker<Reply>().RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Content, f => f.Lorem.Sentence())
            .RuleFor(r => r.CreatedAt, f => f.Date.Past())
            .RuleFor(r => r.UserId, f => Guid.NewGuid());
        }

        private static Faker<LikedReply> GenerateLikedReply()
        {
            return new Faker<LikedReply>().RuleFor(lr => lr.Id, f => Guid.NewGuid())
                .RuleFor(lr => lr.ReplyId, f => f.PickRandom(Replies).Id)
                .RuleFor(lr => lr.UserId, f => Guid.NewGuid());
        }

        private static Faker<LikedComment> GenerateLikedComment()
        {
            return new Faker<LikedComment>().RuleFor(lc => lc.Id, f => Guid.NewGuid())
                .RuleFor(lc => lc.CommentId, f => f.PickRandom(GamesComments).Id)
                .RuleFor(lc => lc.UserId, f => Guid.NewGuid());
        }

        private static Faker<GameImage> GenerateGameImage()
        {
            return new Faker<GameImage>().RuleFor(gi => gi.Id, f => Guid.NewGuid())
                .RuleFor(gi => gi.GameId, f => f.PickRandom(Games).Id)
                .RuleFor(gi => gi.Directory, f => f.System.FilePath());
        }

        private static Faker<CommentReply> GenerateCommentReply()
        {
            return new Faker<CommentReply>().RuleFor(cr => cr.Id, f => Guid.NewGuid())
                .RuleFor(cr => cr.CommentId, f => f.PickRandom(GamesComments).Id)
                .RuleFor(cr => cr.ReplyId, f => f.PickRandom(Replies).Id);
        }
    }
}