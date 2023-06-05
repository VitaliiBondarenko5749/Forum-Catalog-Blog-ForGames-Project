using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Catalog_of_Games_API.Controllers
{
    [Route("/settings")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILogger<LanguageController> logger;
        private readonly ILanguageService languageService;

        public LanguageController(ILogger<LanguageController> logger, ILanguageService languageService)
        {
            this.logger = logger;
            this.languageService = languageService;
        }

        [HttpGet("add-language")]
        [Authorize]
        public async Task<ActionResult<List<string>>> FindLanguagesByNameAsync(string languageName)
        {
            try
            {
                List<string> languages = await languageService.FindByNameAsync(languageName);

                if (languages.IsNullOrEmpty())
                {
                    return NotFound();
                }

                return Ok(languages);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("add-language")]
        [Authorize]
        public async Task<IActionResult> AddLanguageAsync([FromBody] LanguageInsertDto languageDto)
        {
            try
            {
                await languageService.AddAsync(languageDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete-language/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLanguageAsync(Guid id)
        {
            try
            {
                await languageService.DeleteAsync(id);

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