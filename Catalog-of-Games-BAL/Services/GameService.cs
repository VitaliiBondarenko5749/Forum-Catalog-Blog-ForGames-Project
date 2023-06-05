using AutoMapper;
using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Validators;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using FluentValidation.Results;
using System;
using System.Text;

#pragma warning disable

namespace Catalog_of_Games_BAL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Отримуємо всі ігри конкретної категорії.
        public async Task<List<ShortGameInfoDto>> GetGamesByCategoryAsync(string categoryName, int pageNumber, int pageSize)
        {
            List<Game>? games = await unitOfWork.GameRepository.GetGamesByCategoryAsync(categoryName, pageNumber, pageSize)
                ?? throw new NullReferenceException($"There's no games with category : {categoryName}");

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.CreateMap<Game, ShortGameInfoDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            List<ShortGameInfoDto> gameDtos = mapper.Map<List<ShortGameInfoDto>>(games);

            return gameDtos;
        }

        // Отримуємо інформацію про конкретну гру(шукаємо за іменем)
        public async Task<GameInfoDto> GetGameByNameAsync(string categoryName, string gameName)
        {
            Game game = await unitOfWork.GameRepository.GetGameByNameAsync(categoryName, gameName)
                ?? throw new NullReferenceException($"Game with name {gameName} was not found!");

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.CreateMap<Game, GameInfoDto>()
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.ToString("yyyy-dd-MM")));

                cfg.CreateMap<Publisher, PublisherInfoDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            GameInfoDto gameInfoDto = mapper.Map<GameInfoDto>(game);

            if (game.GameCategories is not null)
            {
                gameInfoDto.Categories = new List<ShortCategoryInfoDto>();

                foreach (GameCategory gameCategory in game.GameCategories)
                {
                    ShortCategoryInfoDto categoryInfoDto = new()
                    {
                        Id = gameCategory.Category.Id,
                        Name = gameCategory.Category.Name
                    };

                    gameInfoDto.Categories.Add(categoryInfoDto);
                }
            }

            if (game.GameDevelopers is not null)
            {
                gameInfoDto.Developers = new List<DeveloperInfoDto>();

                foreach (GameDeveloper gameDeveloper in game.GameDevelopers)
                {
                    DeveloperInfoDto developerInfoDto = new()
                    {
                        Id = gameDeveloper.Developer.Id,
                        Name = gameDeveloper.Developer.Name
                    };

                    gameInfoDto.Developers.Add(developerInfoDto);
                }
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (game.GameLanguages is not null)
            {
                gameInfoDto.Languages = new List<LanguageInfoDto>();

                foreach (GameLanguage gameLanguage in game.GameLanguages)
                {
                    LanguageInfoDto languageInfoDto = new()
                    {
                        Id = gameLanguage.Language.Id,
                        Name = gameLanguage.Language.Name
                    };

                    gameInfoDto.Languages.Add(languageInfoDto);
                }
            }

            if (game.GamePlatforms is not null)
            {
                gameInfoDto.Platforms = new List<PlatformInfoDto>();

                foreach (GamePlatform gamePlatform in game.GamePlatforms)
                {
                    PlatformInfoDto platformInfoDto = new()
                    {
                        Id = gamePlatform.Platform.Id,
                        Name = gamePlatform.Platform.Name
                    };

                    gameInfoDto.Platforms.Add(platformInfoDto);
                }
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return gameInfoDto;
        }

        // Отримуємо всі можливі ігри
        public async Task<List<ShortGameInfoDto>> GetAllGamesAsync(int pageNumber, int pageSize)
        {
            List<Game> games = await unitOfWork.GameRepository.GetAllAsync(pageNumber, pageSize)
                ?? throw new NullReferenceException("There's no games in the database");

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.CreateMap<Game, ShortGameInfoDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            List<ShortGameInfoDto> gameDtos = mapper.Map<List<ShortGameInfoDto>>(games);

            return gameDtos;
        }

        // Отримуємо всі можливі ігри за іменем
        public async Task<List<ShortGameInfoDto>> FindByNameAsync(int pageNumber, int pageSize, string gameName)
        {
            List<Game> games = await unitOfWork.GameRepository.FindByNameAsync(pageNumber, pageSize, gameName)
                ?? throw new InvalidDataException($"There are no such games with name: {gameName}");

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.CreateMap<Game, ShortGameInfoDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            List<ShortGameInfoDto> gameDtos = mapper.Map<List<ShortGameInfoDto>>(games);

            return gameDtos;
        }

        // Додаємо гру
        public async Task AddAsync(GameInsertDto gameDto)
        {
            GameValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(gameDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (FluentValidation.Results.ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Game game = new()
            {
                Id = Guid.NewGuid(),
                Name = gameDto.Name,
                Rating = gameDto.Rating,
                Description = gameDto.Description ?? throw new InvalidDataException("Description cannot be empty"),
                MainImage = string.Empty
            };

            Publisher publisher = await unitOfWork.PublisherRepository.FindByNameAsync(gameDto.PublisherName);

            if (publisher is null)
            {
                publisher = new()
                {
                    Id = Guid.NewGuid(),
                    Name = gameDto.PublisherName,
                };

                game.Publisher = publisher;
            }
            else
            {
                game.PublisherId = publisher.Id;
            }

            foreach (string category in gameDto.Categories)
            {
                Category categoryObj = await unitOfWork.CategoryRepository.FindByNameAsync(category);

                if (categoryObj is null)
                {
                    continue;
                }

                if (game.GameCategories is null)
                {
                    game.GameCategories = new List<GameCategory>();
                }

                GameCategory gameCategory = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    CategoryId = categoryObj.Id
                };

                game.GameCategories.Add(gameCategory);
            }

            foreach (string developer in gameDto.Developers)
            {
                Developer developerObj = await unitOfWork.DeveloperRepository.FindByNameAsync(developer);

                if (developerObj is null)
                {
                    continue;
                }

                if (game.GameDevelopers is null)
                {
                    game.GameDevelopers = new List<GameDeveloper>();
                }

                GameDeveloper gameDeveloper = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    DeveloperId = developerObj.Id,
                };

                game.GameDevelopers.Add(gameDeveloper);
            }

            foreach (string platform in gameDto.Platforms)
            {
                Platform platformObj = await unitOfWork.PlatformRepository.FindByNameAsync(platform);

                if (platformObj is null)
                {
                    continue;
                }

                if (game.GamePlatforms is null)
                {
                    game.GamePlatforms = new List<GamePlatform>();
                }

                GamePlatform gamePlatform = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    PlatformId = platformObj.Id
                };

                game.GamePlatforms.Add(gamePlatform);
            }

            foreach (string language in gameDto.Languages)
            {
                Language languageObj = await unitOfWork.LanguageRepository.FindByNameAsync(language);

                if (languageObj is null)
                {
                    continue;
                }

                if (game.GameLanguages is null)
                {
                    game.GameLanguages = new List<GameLanguage>();
                }

                GameLanguage gameLanguage = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    LanguageId = languageObj.Id
                };

                game.GameLanguages.Add(gameLanguage);
            }

            await unitOfWork.GameRepository.CreateAsync(game);

            await unitOfWork.SaveChangesAsync();
        }

        // Оновлюємо гру
        public async Task UpdateAsync(Guid id, GameInsertDto gameDto)
        {
            GameValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(gameDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (FluentValidation.Results.ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Game game = await unitOfWork.GameRepository.GetByIdAsync(id)
                ?? throw new InvalidDataException("There's no game in the database");

            game.Name = gameDto.Name;
            game.Rating = gameDto.Rating;
            game.Description = gameDto.Description;

            Publisher publisher = await unitOfWork.PublisherRepository.FindByNameAsync(gameDto.PublisherName);

            if (publisher is null)
            {
                publisher = new()
                {
                    Id = Guid.NewGuid(),
                    Name = gameDto.PublisherName,
                };

                game.Publisher = publisher;
            }
            else
            {
                game.PublisherId = publisher.Id;
            }

            foreach (string category in gameDto.Categories)
            {
                Category categoryObj = await unitOfWork.CategoryRepository.FindByNameAsync(category);

                if (categoryObj is null)
                {
                    continue;
                }

                if (game.GameCategories is null)
                {
                    game.GameCategories = new List<GameCategory>();
                }

                GameCategory gameCategory = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    CategoryId = categoryObj.Id,
                };

                game.GameCategories.Add(gameCategory);
            }

            foreach (string developer in gameDto.Developers)
            {
                Developer developerObj = await unitOfWork.DeveloperRepository.FindByNameAsync(developer);

                if (developerObj is null)
                {
                    continue;
                }

                if (game.GameDevelopers is null)
                {
                    game.GameDevelopers = new List<GameDeveloper>();
                }

                GameDeveloper gameDeveloper = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    DeveloperId = developerObj.Id,
                };

                game.GameDevelopers.Add(gameDeveloper);
            }

            foreach (string platform in gameDto.Platforms)
            {
                Platform platformObj = await unitOfWork.PlatformRepository.FindByNameAsync(platform);

                if (platformObj is null)
                {
                    continue;
                }

                if (game.GamePlatforms is null)
                {
                    game.GamePlatforms = new List<GamePlatform>();
                }

                GamePlatform gamePlatform = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    PlatformId = platformObj.Id
                };

                game.GamePlatforms.Add(gamePlatform);
            }

            foreach (string language in gameDto.Languages)
            {
                Language languageObj = await unitOfWork.LanguageRepository.FindByNameAsync(language);

                if (languageObj is null)
                {
                    continue;
                }

                if (game.GameLanguages is null)
                {
                    game.GameLanguages = new List<GameLanguage>();
                }

                GameLanguage gameLanguage = new()
                {
                    Id = Guid.NewGuid(),
                    GameId = game.Id,
                    LanguageId = languageObj.Id
                };

                game.GameLanguages.Add(gameLanguage);
            }

            await unitOfWork.GameRepository.UpdateAsync(game);

            await unitOfWork.SaveChangesAsync();
        }

    }
}