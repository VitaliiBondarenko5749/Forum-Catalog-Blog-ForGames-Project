using Catalog_of_Games_BAL.DTOs;
using FluentValidation;

namespace Catalog_of_Games_BAL.Validators
{
    public class GameValidator : AbstractValidator<GameInsertDto>
    {
        public GameValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(70).WithMessage("Name must be less than 70 symbols");

            RuleFor(g => g.PublisherName)
                .NotEmpty().WithMessage("PublisherName cannot be empty")
                .MaximumLength(70).WithMessage("PublisherName must be less than 70 symbols");

            RuleFor(g => g.Rating)
                .InclusiveBetween(0, 10).WithMessage("Rating must be between 0 and 10");

            RuleFor(g => g.Description)
                .MaximumLength(1000).WithMessage("Description must be less than 1000 symbols");

            RuleFor(g => g.Categories)
                .NotEmpty()
                .NotNull().WithMessage("Categories cannot be empty or null");

            RuleFor(g => g.Developers)
               .NotEmpty()
               .NotNull().WithMessage("Developers cannot be empty or null");

            RuleFor(g => g.Platforms)
                .NotEmpty()
                .NotNull().WithMessage("Platforms cannot be empty or null");

            RuleFor(g => g.Languages)
                .NotEmpty()
                .NotNull().WithMessage("Languages cannot be empty or null");
        }
    }
}