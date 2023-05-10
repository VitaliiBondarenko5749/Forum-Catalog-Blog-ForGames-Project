using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Forum_DAL.Models;
using Swashbuckle.AspNetCore.Annotations;
using Forum_BAL.Validators;
using FluentValidation.Results;

namespace Forum_API.Controllers
{

    [Route("/posts")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ILogger<CommentController> logger;
        private readonly ICommentService commentService;

        public CommentController(ILogger<CommentController> logger, ICommentService commentService)
        {
            this.logger = logger;
            this.commentService = commentService;
        }

        // Додання нового коментаря до поста
        [HttpPost("{postId}/comments")]
        public async Task<ActionResult> PostCommentAsync(Guid postId, [FromBody] CommentInsertDTO commentInsertDto)
        {
            try
            {
                commentInsertDto.PostId = postId;

                CommentInsertDtoValidator validator = new();
                ValidationResult result = await validator.ValidateAsync(commentInsertDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                await commentService.AddCommentToPostAsync(commentInsertDto);

                return Ok(StatusCode(StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Видалення коментаря з поста
        [HttpDelete("{postId}/comments/{commentId}")]
        public async Task<ActionResult> DeleteCommentAsync(Guid postId, Guid commentId)
        {
            try
            {
                await commentService.DeleteCommentFromPostAsync(new PostComment { PostId = postId, CommentId = commentId });

                return Ok(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}