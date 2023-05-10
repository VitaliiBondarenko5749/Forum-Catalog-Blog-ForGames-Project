using AutoMapper;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_DAL.Contracts;
using Forum_DAL.Models;

namespace Forum_BAL.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Отримуємо всі пости та ігри, які пов'язані до кожного поста
        public async Task<IEnumerable<ShortPostInfoDTO>> GetAllPostsAsync()
        {
            // Отримання всіх постів
            IEnumerable<Post> posts = await unitOfWork.PostRepository.GetAllAsync();

            // Проходимо ітерацію по всіх постах, для того щоб отримати колекцію ігор, які зв'язані з постом
            foreach (Post post in posts)
            {
                // Отримання колекції ігор для окремого поста
                post.Games = (List<Game>)await unitOfWork.GameRepository.GetAllGamesForPostAsync(post.Id);
            }

            MapperConfiguration configuration = new(cfg =>
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
        public async Task<ConcretePostInfoDTO> GetPostAsync(Guid postId)
        {
            // Отримуємо конкретний пост
            Post post = await unitOfWork.PostRepository.GetAsync(postId);

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

            MapperConfiguration configuration = new(cfg =>
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
            Post post = new()
            {
                Id = Guid.NewGuid(),
                Title = postInsertDto.Title,
                Content = postInsertDto.Content,
                CreatedAt = DateTime.Now
            };

            // Тепер додаємо новий пост в базу даних
            _ = await unitOfWork.PostRepository.AddAsync(post);

            PostGame postGame = new()
            {
                PostId = post.Id
            };

            if (postInsertDto.Games != null)
            {
                foreach (ShortGameInfoDTO game in postInsertDto.Games)
                {
                    try
                    {
                        if (game.Name != null)
                        {
                            // Пробуємо знайти гру за іменем з таблиці Games, та повернути її id.
                            // Якщо гру не знайде -  і нас перекине в catch і почнеться нова ітерація
                            postGame.GameId = await unitOfWork.GameRepository.GetGameIdByNameAsync(game.Name);

                            // У разі знаходження гри, вставляємо значення id в проміжну таблицю PostsGames
                            await unitOfWork.PostGameRepository.AddAsync(postGame);
                        }
                    }
                    catch { continue; }
                }
            }

            unitOfWork.Commit();
        }

        // Оновлюємо пост
        public async Task UpdatePostAsync(PostInsertUpdateDTO postUpdateDto)
        {
            // Отримуємо пост з бази даних + перевіряємо чи є взагалі такий пост(якщо нема - буде вийняток).
            Post post = await unitOfWork.PostRepository.GetAsync(postUpdateDto.Id);

            // Присвоюємо посту нову інформацію
            post.Title = postUpdateDto.Title;
            post.Content = postUpdateDto.Content;

            //Створюємо колекцію int, яка зберігає нам id ігор, пов'язаних з постом
            ICollection<Guid> gamesId = new List<Guid>();

            if (postUpdateDto.Games != null)
            {
                foreach (ShortGameInfoDTO gameInfoDTO in postUpdateDto.Games)
                {
                    if (gameInfoDTO.Name != null)
                    {
                        try
                        {
                            // Шукаємо гру за іменем і отримуємо id гри. Якщо нам не знайде - вийняток і нова ітерація
                            Guid gameId = await unitOfWork.GameRepository.GetGameIdByNameAsync(gameInfoDTO.Name);

                            // У разі знаходження додаємо айді до колекції
                            gamesId.Add(gameId);
                        }
                        catch { continue; }
                    }
                }
            }

            // Створюємо екземпляр класу PostGame, для передачі параметрів в методи обробки запиту
            PostGame postGameParam = new() { PostId = post.Id };

            if (gamesId != null)
            {
                foreach (Guid gameId in gamesId)
                {
                    try
                    {
                        postGameParam.GameId = gameId;

                        // Находимо, чи є зв'язки між грою та постом
                        PostGame postGame = await unitOfWork.PostGameRepository.
                            GetConnectedPostAndGameAsync(postGameParam);
                    }
                    catch
                    {
                        // Якщо зв'язку немає - додаємо новий зв'язок
                        _ = unitOfWork.PostGameRepository.AddAsync(postGameParam);
                    }
                }
            }

            // Отримуємо колекцію всіх GameId пов'язаних з постом
            ICollection<Guid> allGamesId = (List<Guid>)await unitOfWork.PostGameRepository.GetGamesIdAsync(post.Id);

            foreach (Guid gameId in allGamesId)
            {
                if (gamesId != null && !gamesId.Contains(gameId))
                {
                    postGameParam.GameId = gameId;

                    await unitOfWork.PostGameRepository.DeletePostGameAsync(postGameParam);
                }
            }

            await unitOfWork.PostRepository.ReplaceAsync(post);

            unitOfWork.Commit();
        }

        // Видаляємо пост
        public async Task DeletePostAsync(Guid postId)
        {  
            // Знаходимо пост за айді. У разі, якщо не знайде - викине вийняток 
            _ = await unitOfWork.PostRepository.GetAsync(postId);

            // Отримання всіх CommentId, які пов'язані з постом
            IEnumerable<Guid> commentIds = await unitOfWork.PostCommentRepository.GetCommentsIdAsync(postId);

            List<Guid>? allReplyIds = null;

            // Проходимо ітерацію по кожному commentId
            foreach (Guid commentId in commentIds)
            {
                // Отримання всіх ReplyId, які пов'язані з коментом
                List<Guid> repliesId = (List<Guid>)await unitOfWork.CommentReplyRepository.GetRepliesIdAsync(commentId);

                allReplyIds ??= new List<Guid>(); 

                allReplyIds.AddRange(repliesId);

                // Видаляємо коментар
                await unitOfWork.CommentRepository.DeleteAsync(commentId);
            }

            if (allReplyIds is not null)
            {
                foreach (Guid replyId in allReplyIds.Distinct())
                {
                    await unitOfWork.ReplyRepository.DeleteAsync(replyId);
                }
            }

            // Видаляємо пост
            await unitOfWork.PostRepository.DeleteAsync(postId);

            // Підтверджуємо зміни в базі даних
            unitOfWork.Commit();
        }
    }
}