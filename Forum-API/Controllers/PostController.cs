using FluentValidation.Results;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_BAL.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API.Controllers
{
    [Route("/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> logger;
        private readonly IPostService postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            this.logger = logger;
            this.postService = postService;
        }

        [HttpGet] // Get all posts
        public async Task<ActionResult<IEnumerable<ShortPostInfoDTO>>> GetAllPostsAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                IEnumerable<ShortPostInfoDTO> postsDto = await postService.GetAllPostsAsync(pageNumber, pageSize);

                return Ok(postsDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{postId}")] // Get post by Id
        public async Task<ActionResult<ConcretePostInfoDTO>> GetPostAsync(Guid postId)
        {
            try
            {
                ConcretePostInfoDTO postDto = await postService.GetPostAsync(postId);

                if (postDto == null)
                {
                    logger.LogInformation($"Post with id: {postId}, was not found in the database.");

                    return NotFound();
                }

                return Ok(postDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost] // Add a new post
        public async Task<ActionResult> AddPostAsync([FromBody] PostInsertUpdateDTO postDto)
        {
            try
            {
                await postService.AddPostAsync(postDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{postId}")] // Delete post by id
        public async Task<ActionResult> DeletePostAsync(Guid postId)
        {
            try
            {
                await postService.DeletePostAsync(postId);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}