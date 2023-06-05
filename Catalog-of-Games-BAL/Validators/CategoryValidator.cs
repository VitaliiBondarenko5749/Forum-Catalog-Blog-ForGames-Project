using Catalog_of_Games_BAL.DTOs;
using FluentValidation;

namespace Catalog_of_Games_BAL.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryInsertDto>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(70).WithMessage("Name must be less than 70 symbols");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description")
                .MaximumLength(1000).WithMessage("Description must be less than 1000 symbols");
        }
    }
}