using Forum_BAL.Contracts;
using Forum_BAL.DTO;
using Microsoft.AspNetCore.Mvc;
using Forum_DAL.Models;
using Forum_BAL.Validators;
using FluentValidation.Results;

namespace Forum_API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class ReplyToCommentController : ControllerBase
    {
        private readonly ILogger<ReplyToCommentController> logger;
        private readonly IReplyToCommentService replyToCommentService;

        public ReplyToCommentController(ILogger<ReplyToCommentController> logger, IReplyToCommentService replyToCommentService)
        {
            this.logger = logger;
            this.replyToCommentService = replyToCommentService;
        }

        [HttpPost("{postId}/comments/{commentId}/replies")]
        public async Task<ActionResult> PostReplyAsync(Guid postId, Guid commentId, [FromBody] ReplyInsertDTO replyInsertDto)
        {
            try
            {
                replyInsertDto.PostId = postId;
                replyInsertDto.CommentId = commentId;

                ReplyInsertDtoValidator validator = new();
                ValidationResult result = await validator.ValidateAsync(replyInsertDto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                await replyToCommentService.AddReplyToCommentAsync(replyInsertDto);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                logger.LogError($"Transaction fail! Something went wrong in \"PostReplyAsync(...)\" method. Error type:" +
                    $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{postId}/comments/{commentId}/replies")]
        public async Task<ActionResult> DeleteReplyAsync(Guid postId, Guid commentId, Guid replyId)
        {
            try
            {
                await replyToCommentService.DeleteReplyFromCommentAsync(new PostComment 
                { PostId = postId, CommentId = commentId }, replyId);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch(Exception ex)
            {
                logger.LogError($"Transaction fail! Something went wrong in \"DeleteReplyAsync(...)\" method. Error type:" +
                    $" {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}