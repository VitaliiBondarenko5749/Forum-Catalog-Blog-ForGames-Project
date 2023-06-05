using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_of_Games_API.Controllers
{
    [Route("/settings")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly ILogger<PlatformController> logger;
        private readonly IPlatformService platformService;

        public PlatformController(ILogger<PlatformController> logger, IPlatformService platformService)
        {
            this.logger = logger;
            this.platformService = platformService;
        }

        [HttpGet("add-platform")]
        [Authorize]
        public async Task<ActionResult<List<string>>> FindPlatformsByNameAsync(string platformName)
        {
            try
            {
                List<string> platforms = await platformService.FindByNameAsync(platformName);

                if(platforms is null)
                {
                    return NotFound();
                }

                return Ok(platforms);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("add-platform")]
        [Authorize]
        public async Task<IActionResult> AddPlatformAsync([FromBody] PlatformInsertDto platformDto)
        {
            try
            {
                await platformService.AddAsync(platformDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("add-platform/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePlatfromAsync(Guid id)
        {
            try
            {
                await platformService.DeleteAsync(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}