using AutoMapper;
using Catalog_of_Games_DAL.Entities;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_DAL.Contracts;
using Forum_DAL.Models;


namespace Forum_BAL.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork unitOfWork { get; }

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Отримуємо всі пости та ігри, які пов'язані до кожного поста
        public async Task<IEnumerable<ShortPostInfoDTO>> GetAllPostsAndGamesAsync()
        {
            // Отримання всіх постів
            IEnumerable<Post> posts = await unitOfWork.PostRepository.GetAllAsync();

            // Проходимо ітерацію по всіх постах, для того щоб отримати колекцію ігор, які зв'язані з постом
            foreach (Post post in posts)
            {
                // Отримання колекції ігор для окремого поста
                post.Games = (List<Game>)await unitOfWork.GameRepository.GetAllGamesForPostAsync(post.Id);
            }

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, ShortPostInfoDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-dd-MM")));

                cfg.CreateMap<Game, ShortGameInfoDTO>();
            });

            IMapper mapper = configuration.CreateMapper();

            IEnumerable<ShortPostInfoDTO> postsDto = mapper.Map<IEnumerable<ShortPostInfoDTO>>(posts);

            return postsDto;
        }

        /* Отримуємо інформацію про конкретний пост та всю інформацію про нього:
         * Коментарі
         * Відповіді на коментар */
        public async Task<ConcretePostInfoDTO> GetPostAsync(int postId)
        {
            // Отримуємо конкретний пост
            Post post = await unitOfWork.PostRepository.GetAsync(postId);

            // Перевіряємо, чи отримали ми якийсь пост. Якщо нічого - виходимо з методу.
            if (post == null)
            {
                throw new Exception("Couldn't get Post by id.");
            }

            // Отримуємо ігри, які пов'язані з постом
            post.Games = (List<Game>)await unitOfWork.GameRepository.GetAllGamesForPostAsync(postId);

            // Отримуємо коментарі, які пов'язані з постом
            post.Comments = (List<Comment>)await unitOfWork.CommentRepository.GetAllCommentsForPostAsync(postId);

            //Проходимо ітерацію, щоб отримати лайки на коментарі, а також всю інформацію про відповіді на коментарі
            foreach (Comment comment in post.Comments.Distinct())
            {
                // Отримуємо лайки для коментаря
                comment.NumberOfLikes = await unitOfWork.LikedCommentRepository.GetLikesForCommentAsync(comment.Id);

                // Отримуємо відповіді на коментар
                comment.Replies = (List<Reply>)await unitOfWork.ReplyRepository.GetAllRepliesForCommentAsync(comment.Id);

                // Проходимо ітерацію, щоб отримати лайки для кожної відповіді на коментар
                foreach (Reply reply in comment.Replies.Distinct())
                {
                    // Отримуємо лайки для відповіді
                    reply.NumberOfLikes = await unitOfWork.LikedReplyRepository.GetLikesForReplyAsync(reply.Id);
                }
            }

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, ConcretePostInfoDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-dd-MM")));

                cfg.CreateMap<Game, ShortGameInfoDTO>();

                cfg.CreateMap<Comment, CommentInfoDTO>()
                .ForMember(dest => dest.WhenReplied, opt => opt.MapFrom(src => src.WhenReplied.ToString("yyyy-dd-MM")));

                cfg.CreateMap<Reply, ReplyInfoDTO>()
                .ForMember(dest => dest.WhenReplied, opt => opt.MapFrom(src => src.WhenReplied.ToString("yyyy-dd-MM")));
            });

            IMapper mapper = configuration.CreateMapper();

            ConcretePostInfoDTO postDto = mapper.Map<ConcretePostInfoDTO>(post);

            return postDto;
        }

        // Вставляємо новий пост
        public async Task AddPostAsync(PostInsertUpdateDTO postInsertDto)
        {
            // Ініціалізуємо пост
            Post post = new Post()
            {
                Title = postInsertDto.Title,
                Content = postInsertDto.Content,
                CreatedAt = DateTime.Now
            };

            // Перевіряємо чи передаються якісь ігри з DTO
            if (postInsertDto.Games != null)
            {
                // Ініціалізуємо колекцію ігор, новим списком щоб додавати туди ігри
                post.Games = new List<Game>();

                // Проходимо ітерацію для запису інформації з DTO в модель
                foreach (ShortGameInfoDTO gameInfoDTO in postInsertDto.Games)
                {
                    post.Games.Add(new Game { Name = gameInfoDTO.Name });
                }
            }

            PostGame postGame = new PostGame()
            {
                // Тепер додаємо новий пост в базу даних + отримуємо айді нового поста 
                PostId = await unitOfWork.PostRepository.AddAsync(post)
            };

            if (post.Games != null)
            {
                foreach (Game game in post.Games)
                {
                    if (game.Name != null)
                    {
                        // Пробуємо знайти гру за іменем з таблиці Games, та повернути її id.
                        // Якщо гру не знайде -  і нас перекине в catch і почнеться нова ітерація
                        postGame.GameId = await unitOfWork.GameRepository.GetGameByNameAsync(game.Name);

                        if(postGame.GameId == 0) { continue; }

                        // У разі знаходження гри, вставляємо значення id в проміжну таблицю PostsGames
                        await unitOfWork.PostGameRepository.AddAsync(postGame);
                    }
                }
            }
        }

        // Оновлюємо пост
        public async Task UpdatePostAsync(PostInsertUpdateDTO postUpdateDto)
        {
            // Отримуємо пост з бази даних + перевіряємо чи є взагалі такий пост(якщо нема - буде вийняток).
            Post post = await unitOfWork.PostRepository.GetAsync(postUpdateDto.Id) ??
                throw new Exception($"Post with id: {postUpdateDto.Id} does not exist.");

            // Присвоюємо посту нову інформацію
            post.Id = postUpdateDto.Id;
            post.Title = postUpdateDto.Title;
            post.Content = postUpdateDto.Content;

            //Створюємо колекцію int, яка зберігає нам id ігор
            ICollection<int> gamesId = new List<int>();

            if (postUpdateDto.Games != null)
            {
                foreach (ShortGameInfoDTO gameInfoDTO in postUpdateDto.Games)
                {
                    if (gameInfoDTO.Name != null)
                    {
                        try
                        {
                            // Шукаємо гру за іменем і отримуємо id гри. Якщо нам не знайде - вийняток і нова ітерація
                            int gameId = await unitOfWork.GameRepository.GetGameByNameAsync(gameInfoDTO.Name);

                            // У разі знаходження додаємо айді до колекції
                            gamesId.Add(gameId);
                        }
                        catch { continue; }
                    }
                }
            }

            if (gamesId != null)
            {
                foreach (int gameId in gamesId)
                {
                    try
                    {
                        // Находимо, чи є зв'язки між грою та постом
                        PostGame postGame = await unitOfWork.PostGameRepository.
                            GetConnectedPostAndGameAsync(new PostGame { PostId = post.Id, GameId = gameId });
                    }
                    catch
                    {
                        // Якщо зв'язку немає - додаємо новий зв'язок
                        _ = unitOfWork.PostGameRepository.AddAsync(new PostGame { PostId = post.Id, GameId = gameId });
                    }
                }
            }

            // Отримуємо колекцію всіх GameId пов'язаних з постом
            ICollection<int> allGamesId = (List<int>)await unitOfWork.PostGameRepository.GetGamesIdAsync(post.Id);

            foreach (int gameId in allGamesId)
            {
                if (gamesId != null && !gamesId.Contains(gameId))
                {
                    await unitOfWork.PostGameRepository.
                        DeletePostGameAsync(new PostGame { PostId = post.Id, GameId = gameId });
                }
            }

            await unitOfWork.PostRepository.ReplaceAsync(post);
        }

        // Видаляємо пост
        public async Task DeletePostAsync(int postId)
        {
            // Знаходимо пост за айді. У разі, якщо не знайде - викине вийняток 
            Post post = await unitOfWork.PostRepository.GetAsync(postId);

            // Отримання всіх CommentId, які пов'язані з постом
            IEnumerable<int> commentIds = await unitOfWork.PostCommentRepository.GetCommentsIdAsync(postId);

            // Проходимо ітерацію по кожному commentId
            foreach (int commentId in commentIds)
            {
                // Отримання всіх ReplyId, які пов'язані з коментом
                IEnumerable<int> repliesId = await unitOfWork.CommentReplyRepository.GetRepliesIdAsync(commentId);

                // Проходимо ітерацію по кожному знайденому ReplyId
                foreach (int replyId in repliesId)
                {
                    // Видаляємо відповідь на комент
                    await unitOfWork.ReplyRepository.DeleteAsync(replyId);
                }

                // Видаляємо коментар
                await unitOfWork.CommentRepository.DeleteAsync(commentId);
            }

            // Видаляємо пост
            await unitOfWork.PostRepository.DeleteAsync(postId);
        }
    }
}