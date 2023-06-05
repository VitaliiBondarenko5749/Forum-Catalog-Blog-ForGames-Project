using Catalog_of_Games_BAL.DTOs;
using FluentValidation;

namespace Catalog_of_Games_BAL.Validators
{
    public class DeveloperValidator : AbstractValidator<DeveloperInsertDto>
    {
        public DeveloperValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name cannot be empty")
                .MaximumLength(70).WithMessage("Name must be less than 70 symbols");
        }
    }
}