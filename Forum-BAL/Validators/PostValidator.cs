using FluentValidation;
using Forum_BAL.DTO;

namespace Forum_BAL.Validators
{
    public class PostValidator : AbstractValidator<PostInsertUpdateDTO>
    {
        public PostValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id cannot be null or empty.");

            RuleFor(p => p.Title)
                .NotNull()
                .WithMessage("Title cannot be nullable.")
                .MaximumLength(200)
                .WithMessage("MaximumLength is 200 symbols.")
                .Matches(@"^[A-Za-z0-9\s]+$")
                .WithMessage("Title should match the specified pattern.");

            RuleFor(p => p.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Content cannot be null or empty.")
                .MaximumLength(4000)
                .WithMessage("MaximumLength is 4000 symbols.")
                .Matches(@"^[A-Za-z0-9\s]+$")
                .WithMessage("Content should match the specified pattern.");

        }
    }
}