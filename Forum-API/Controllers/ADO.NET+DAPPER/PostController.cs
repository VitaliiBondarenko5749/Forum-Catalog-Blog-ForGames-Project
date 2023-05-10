using FluentValidation.Results;
using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Forum_BAL.Validators;
using Forum_DAL.Paging;
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

        [HttpGet] // Getting all posts
        public async Task<ActionResult<IEnumerable<ShortPostInfoDTO>>> GetAllPostsAsync(int pageNumber = 1)
        {
            try
            {
                const int PAGE_SIZE = 10; // Кількість елементів на сторінку
                                          
                IEnumerable<ShortPostInfoDTO> postsDto = await postService.GetAllPostsAsync();

                PagedList<ShortPostInfoDTO> items = PagedList<ShortPostInfoDTO>.ToPagedList(postsDto, pageNumber, PAGE_SIZE);

                return Ok(items);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{postId}")] // Getting one post by Id
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

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost] // Adding a new post
        public async Task<ActionResult> AddPostAsync([FromBody] PostInsertUpdateDTO postDto)
        {
            try
            {
                bool isValid = await ValidatePostAsync(postDto);

                if (!isValid)
                {
                    logger.LogInformation("Incorrect client-side json!");

                    return BadRequest("Incorrect object \"Post\" type!");
                }

                await postService.AddPostAsync(postDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{postId}")] // Updating post by Id
        public async Task<ActionResult> UpdatePostAsync(Guid postId, [FromBody] PostInsertUpdateDTO postUpdateDto)
        {
            try
            {
               
                postUpdateDto.Id = postId;

                bool isValid = await ValidatePostAsync(postUpdateDto);

                if (!isValid)
                {
                    logger.LogInformation("Incorrect client-side json!");

                    return BadRequest("Incorrect object \"Post\" type!");
                }

                await postService.UpdatePostAsync(postUpdateDto);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        private async Task<bool> ValidatePostAsync(PostInsertUpdateDTO value)
        {
            PostInsertUpdateDtoValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(value);

            if (!result.IsValid)
            {
                return false;
            }

            return true;
        }
    }
}