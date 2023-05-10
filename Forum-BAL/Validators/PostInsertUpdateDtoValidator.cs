using FluentValidation;
using Forum_BAL.DTO;

namespace Forum_BAL.Validators
{
    public class PostInsertUpdateDtoValidator : AbstractValidator<PostInsertUpdateDTO>
    {
        public PostInsertUpdateDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id cannot be null or empty.");

            RuleFor(p => p.Title)
                .NotNull()
                .WithMessage("Title cannot be nullable.")
                .MaximumLength(200)
                .WithMessage("MaximumLength is 200 symbols.");

            RuleFor(p => p.Content)
                .NotNull()
                .NotEmpty()
                .WithMessage("Content cannot be null or empty.")
                .MaximumLength(4000)
                .WithMessage("MaximumLength is 4000 symbols.");

        }
    }
}