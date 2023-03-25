using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API.Controllers
{
    [Route("api/posts")]
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

        [HttpGet] // Отримуємо всі пости та ігри, які пов'язані до кожного поста
        public async Task<ActionResult<IEnumerable<ShortPostInfoDTO>>> GetAllPostsAsync()
        {
            try
            {
                IEnumerable<ShortPostInfoDTO> postsDto = await postService.GetAllPostsAndGamesAsync();

                return Ok(postsDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"Transaction fail! Something went wrong in \"GetAllPostsAsync()\" method. Error type:" +
                    $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "error");
            }
        }

        [HttpGet("{postId}")] // Отримуємо інформацію про конкретний пост та всю інформацію про нього
        public async Task<ActionResult<ConcretePostInfoDTO>> GetPostAsync(int postId)
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
                logger.LogError($"Transaction fail! Something went wrong in \"GetPostsAsync()\" method. Error type:" +
                   $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "error");
            }
        }

        [HttpPost] // Додаємо новий пост
        public async Task<ActionResult> AddPostAsync([FromBody] PostInsertUpdateDTO postDto)
        {
            try
            {
                if (postDto == null)
                {
                    logger.LogInformation("Empty client-side json!");

                    return BadRequest("Object \"Post\" type is null.");
                }

                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Incorrect client-side json!");

                    return BadRequest("Incorrect object \"Post\" type!");
                }

                await postService.AddPostAsync(postDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                logger.LogError($"Transaction fail! Something went wrong in \"GetPostsAsync()\" method. Error type:" +
                  $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "error");
            }
        }

        [HttpPut("{postId}")] // Оновлюємо інформацію в пості
        public async Task<ActionResult> UpdatePostAsync(int postId, [FromBody] PostInsertUpdateDTO postUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogInformation("Incorrect client-side json!");

                    return BadRequest("Incorrect object \"Post\" type!");
                }

                postUpdateDto.Id = postId;

                await postService.UpdatePostAsync(postUpdateDto);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Transaction fail! Something went wrong in \"GetPostsAsync()\" method. Error type:" +
                 $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "error");
            }
        }

        [HttpDelete("{postId}")] // Видаляємо пост
        public async Task<ActionResult> DeletePostAsync(int postId)
        {
            try
            {
                await postService.DeletePostAsync(postId);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Transaction fail! Something went wrong in \"DeleteByIdAsync(...)\" method. Error type:" +
                   $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, "error");
            }
        }
    }
}