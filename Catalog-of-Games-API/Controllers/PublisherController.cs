using Catalog_of_Games_BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_of_Games_API.Controllers
{
    [Route("/settings")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly ILogger<PublisherController> logger;
        private readonly PublisherService publisherService;

        public PublisherController(ILogger<PublisherController> logger, PublisherService publisherService)
        {
            this.logger = logger;
            this.publisherService = publisherService;
        }

        [HttpDelete("delete-publisher/{name}")]
        public async Task<IActionResult> DeletePublisherByNameAsync(string name)
        {
            try
            {
                await publisherService.DeleteByNameAsync(name);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}