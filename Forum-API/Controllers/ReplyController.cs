using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Forum_DAL.Models;

namespace Forum_API.Controllers
{
    [Route("/posts")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly ILogger<ReplyController> logger;
        private readonly IReplyService replyToCommentService;

        public ReplyController(ILogger<ReplyController> logger, IReplyService replyToCommentService)
        {
            this.logger = logger;
            this.replyToCommentService = replyToCommentService;
        }

        [HttpPost("{postId}/comments/{commentId}/replies")]
        public async Task<ActionResult> AddReplyAsync(Guid postId, Guid commentId, [FromBody] ReplyInsertDTO replyInsertDto)
        {
            try
            {
                replyInsertDto.PostId = postId;
                replyInsertDto.CommentId = commentId;

                await replyToCommentService.AddReplyAsync(replyInsertDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{postId}/comments/{commentId}/replies")]
        public async Task<ActionResult> DeleteReplyAsync(Guid postId, Guid commentId, Guid replyId)
        {
            try
            {
                await replyToCommentService.DeleteReplyAsync(new PostComment
                { PostId = postId, CommentId = commentId }, replyId);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{postId}/comments/{commentId}/replies/add-like")]
        public async Task<IActionResult> AddLikeToReplyAsync(Guid postId, Guid commentId, LikedReply likedReply)
        {
            try
            {
                PostComment postComment = new() { PostId = postId, CommentId = commentId };

                await replyToCommentService.AddLikeAsync(postComment, likedReply);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{postId}/comments/{commentId}/replies/delete-like")]
        public async Task<IActionResult> DeleteLikeFromReplyAsync(Guid postId, Guid commentId, LikedReply likedReply)
        {
            try
            {
                PostComment postComment = new() { PostId = postId, CommentId = commentId };

                await replyToCommentService.DeleteLikeAsync(postComment, likedReply);

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