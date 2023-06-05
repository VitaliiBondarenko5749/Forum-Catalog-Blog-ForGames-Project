using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_of_Games_API.Controllers
{
    [Route("/categories")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> logger;
        private readonly IGameService gameService;

        public GameController(ILogger<GameController> logger, IGameService gameService)
        {
            this.logger = logger;
            this.gameService = gameService;
        }

        [HttpGet("{categoryName}")] // Get games by category name
        public async Task<ActionResult<List<ShortGameInfoDto>>> GetGamesByCategoryAsync(string categoryName, int pageNumber, int pageSize)
        {
            try
            {
                List<ShortGameInfoDto> gameDtos = await gameService.GetGamesByCategoryAsync(categoryName, pageNumber, pageNumber);

                if (gameDtos is null)
                {
                    return NotFound();
                }

                return Ok(gameDtos);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{categoryName}/{gameName}")] // Get game by name
        public async Task<ActionResult<GameInfoDto>> GetGameByNameAsync(string categoryName, string gameName)
        {
            try
            {
                GameInfoDto gameInfoDto = await gameService.GetGameByNameAsync(categoryName, gameName);

                if (gameInfoDto is null)
                {
                    return NotFound();
                }

                return Ok(gameInfoDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("games")]
        public async Task<ActionResult<List<ShortGameInfoDto>>> GetAllGamesAsync(int pageNumber, int pageSize)
        {
            try
            {
                List<ShortGameInfoDto> gameInfoDtos = await gameService.GetAllGamesAsync(pageNumber, pageSize);

                if (gameInfoDtos is null)
                {
                    return NotFound();
                }

                return Ok(gameInfoDtos);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("games/find-by-name")]
        public async Task<ActionResult<ShortGameInfoDto>> FindGamesByNameAsync(int pageNumber, int pageSize, string gameName)
        {
            try
            {
                List<ShortGameInfoDto> gameDtos = await gameService.FindByNameAsync(pageNumber, pageSize, gameName);

                if (gameDtos is null)
                {
                    return NotFound();
                }

                return Ok(gameDtos);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("/settings/add-game")]
        [Authorize]
        public async Task<IActionResult> AddGameAsync([FromBody] GameInsertDto gameDto)
        {
            try
            {
                await gameService.AddAsync(gameDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpPut("/settings/update-game/{id}")]
        //public async Task<IActionResult> UpdateGameAsync(Guid id, [FromBody] GameInsertDto gameDto)
        //{
        //    try
        //    {
        //        await gameService.UpdateAsync(id, gameDto);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"Error: {ex.Message}");

        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }
}