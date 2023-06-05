using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Forum_DAL.Models;

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
        public async Task<ActionResult> AddCommentAsync(Guid postId, [FromBody] CommentInsertDTO commentInsertDto)
        {
            try
            {
                commentInsertDto.PostId = postId;

                await commentService.AddCommentAsync(commentInsertDto);

                return Ok(StatusCode(StatusCodes.Status201Created));
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Видалення коментаря з поста
        [HttpDelete("{postId}/comments/{commentId}")]
        public async Task<ActionResult> DeleteCommentAsync(Guid postId, Guid commentId)
        {
            try
            {
                await commentService.DeleteCommentAsync(new PostComment { PostId = postId, CommentId = commentId });

                return Ok(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Додаємо лайк до коментаря
        [HttpPost("{postId}/comments/{commentId}/add-like")]
        public async Task<IActionResult> AddLikeToCommentAsync(Guid postId, Guid commentId, LikedComment likedComment)
        {
            try
            {
                PostComment postComment = new()
                {
                    PostId = postId,
                    CommentId = commentId,
                };

                await commentService.AddLikeAsync(postComment, likedComment);

                return Ok();
            }
            catch(Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Видаляємо лайк з коментаря
        [HttpDelete("{postId}/comments/{commentId}/delete-like")]
        public async Task<IActionResult> DeleteLikeFromCommentAsync(Guid postId, Guid commentId, LikedComment likedComment)
        {
            try
            {
                PostComment postComment = new()
                {
                    PostId = postId,
                    CommentId = commentId,
                };

                await commentService.DeleteLikeAsync(postComment, likedComment);

                return Ok();
            }
            catch( Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}