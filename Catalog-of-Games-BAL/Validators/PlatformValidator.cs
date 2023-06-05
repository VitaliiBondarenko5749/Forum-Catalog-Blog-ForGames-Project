using Catalog_of_Games_BAL.DTOs;
using FluentValidation;

namespace Catalog_of_Games_BAL.Validators
{
    public class PlatformValidator : AbstractValidator<PlatformInsertDto>
    {
        public PlatformValidator() 
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Name cannot be empty")
               .MaximumLength(70).WithMessage("Name must be less than 70 symbols");
        }
    }
}