using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_of_Games_API.Controllers
{
    [Route("/settings")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private ILogger<DeveloperController> logger;
        private IDeveloperService developerService;

        public DeveloperController(ILogger<DeveloperController> logger, IDeveloperService developerService)
        {
            this.logger = logger;
            this.developerService = developerService;
        }

        [HttpGet("add-developer")]
        [Authorize]
        public async Task<ActionResult<List<string>>> FindDevelopersByNameAsync(string developerName)
        {
            try
            {
                List<string> developerNames = await developerService.FindByNameAsync(developerName);

                return Ok(developerNames);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("add-developer")]
        [Authorize]
        public async Task<IActionResult> AddDeveloperAsync([FromBody] DeveloperInsertDto developerDto)
        {
            try
            {
                await developerService.AddAsync(developerDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete-developer/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDeveloperAsync(Guid id)
        {
            try
            {
                await developerService.DeleteAsync(id);

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}