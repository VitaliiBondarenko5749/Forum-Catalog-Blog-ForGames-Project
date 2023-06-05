using FluentValidation.Results;
using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Validators;
using Catalog_of_Games_DAL.Repositories.Contracts;
using System.Text;
using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_BAL.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IUnitOfWork unitOfWork;

        public DeveloperService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(DeveloperInsertDto developerDto)
        {
            DeveloperValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(developerDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Developer developer = new()
            {
                Id = Guid.NewGuid(),
                Name = developerDto.Name
            };

            await unitOfWork.DeveloperRepository.CreateAsync(developer);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Developer developer = await unitOfWork.DeveloperRepository.GetByIdAsync(id)
                ?? throw new InvalidDataException("There's no such developer in the database");

            await unitOfWork.DeveloperRepository.DeleteAsync(id);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<string>> FindByNameAsync(string developerName)
        {
            List<string> deleloperNames = await unitOfWork.DeveloperRepository.FindManyByNameAsync(developerName)
                ?? throw new InvalidDataException($"There's no developers with name: {developerName}");

            return deleloperNames;
        }
    }
}