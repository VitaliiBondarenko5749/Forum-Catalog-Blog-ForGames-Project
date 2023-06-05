using AutoMapper;
using FluentValidation.Results;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_BAL.Validators;
using Forum_DAL.Contracts;
using Forum_DAL.Models;
using System.Text;

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
        public async Task<IEnumerable<ShortPostInfoDTO>> GetAllPostsAsync(int pageNumber = 1, int pageSize = 10)
        {
            // Отримання всіх постів
            IEnumerable<Post>? posts = await unitOfWork.PostRepository.GetAllAsync(pageNumber, pageSize);

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
            // Перевіряємо дані
            PostValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(postInsertDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach(ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            // Ініціалізуємо пост
            Post post = new()
            {
                Id = Guid.NewGuid(),
                Title = postInsertDto.Title,
                Content = postInsertDto.Content,
                CreatedAt = DateTime.Now
            };

            // Тепер додаємо новий пост в базу даних
            await unitOfWork.PostRepository.AddAsync(post);

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
                            postGame.Id = Guid.NewGuid();
                            await unitOfWork.PostGameRepository.AddAsync(postGame);
                        }
                    }
                    catch { continue; }
                }
            }

            unitOfWork.Commit();
        }

        // Видаляємо пост
        public async Task DeletePostAsync(Guid postId)
        {  
            // Знаходимо пост за айді. У разі, якщо не знайде - викине вийняток 
            await unitOfWork.PostRepository.GetAsync(postId);

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