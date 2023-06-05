using FluentValidation;
using Forum_BAL.DTO;

namespace Forum_BAL.Validators
{
    public class CommentValidator : AbstractValidator<CommentInsertDTO>
    {
        public CommentValidator() 
        {
            RuleFor(c => c.PostId)
                .NotNull()
                .NotEmpty()
                .WithMessage("PostId cannot be nullable or empty.");

            RuleFor(c => c.Content)
                .MaximumLength(300)
                .WithMessage("MaximumLength is 300 symbols.")
                .NotNull()
                .WithMessage("Content cannot be null.")
                .Matches(@"^[A-Za-z0-9\s]+$")
                .WithMessage("Content should match the specified pattern.");
        }
    }
}