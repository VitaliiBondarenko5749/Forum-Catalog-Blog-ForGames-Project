using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Validators;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using FluentValidation.Results;
using System.Diagnostics.Contracts;
using System.Text;

namespace Catalog_of_Games_BAL.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork unitOfWork;

        public LanguageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(LanguageInsertDto languageDto)
        {
            LanguageValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(languageDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Language language = new()
            {
                Id = Guid.NewGuid(),
                Name = languageDto.Name
            };

            await unitOfWork.LanguageRepository.CreateAsync(language);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Language language = await unitOfWork.LanguageRepository.GetByIdAsync(id)
                ?? throw new InvalidDataException("There's no such developer in the database");

            await unitOfWork.LanguageRepository.DeleteAsync(id);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<string>> FindByNameAsync(string languageName)
        {
            List<string> languages = await unitOfWork.LanguageRepository.FindManyByNameAsync(languageName)
                ?? throw new InvalidDataException($"There are no languages with name: {languageName}");

            return languages;
        }
    }
}