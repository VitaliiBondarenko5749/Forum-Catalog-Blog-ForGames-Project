using FluentValidation;
using Forum_BAL.DTO;

namespace Forum_BAL.Validators
{
    public class ReplyInsertDtoValidator : AbstractValidator<ReplyInsertDTO>
    {
        public ReplyInsertDtoValidator() 
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
                .WithMessage("MaximumLength is 300 symbols.");
        }
    }
}