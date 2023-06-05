using FluentValidation;
using Forum_BAL.DTO;

namespace Forum_BAL.Validators
{
    public class ReplyValidator : AbstractValidator<ReplyInsertDTO>
    {
        public ReplyValidator() 
        {
            RuleFor(r => r.PostId)
                .NotNull()
                .NotEmpty()
                .WithMessage("PostId cannot be null or empty.");

            RuleFor(r => r.CommentId)
                .NotNull()
                .NotEmpty()
                .WithMessage("CommentId cannot be null or empty.");

            RuleFor(r => r.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Content cannot be null or empty.")
                .MaximumLength(300)
                .WithMessage("MaximumLength is 300 symbols.")
                .Matches(@"^[A-Za-z0-9\s]+$")
                .WithMessage("Content should match the specified pattern.");
        }
    }
}