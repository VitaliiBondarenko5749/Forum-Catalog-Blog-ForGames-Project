using FluentValidation;
using Forum_BAL.DTO;

namespace Forum_BAL.Validators
{
    public class CommentInsertDtoValidator : AbstractValidator<CommentInsertDTO>
    {
        public CommentInsertDtoValidator() 
        {
            RuleFor(c => c.PostId)
                .NotNull()
                .NotEmpty()
                .WithMessage("PostId cannot be null or empty.");

            RuleFor(c => c.Content)
                .MaximumLength(300)
                .WithMessage("MaximumLength is 300 symbols.")
                .NotNull()
                .WithMessage("Content cannot be null.");
        }
    }
}