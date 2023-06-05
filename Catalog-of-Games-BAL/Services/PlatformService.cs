using FluentValidation.Results;
using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Validators;
using Catalog_of_Games_DAL.Repositories.Contracts;
using System.Text;
using Catalog_of_Games_DAL.Entities;

namespace Catalog_of_Games_BAL.Services
{
    public class PlatformService : IPlatformService
    {
        private readonly IUnitOfWork unitOfWork;

        public PlatformService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(PlatformInsertDto platformDto)
        {
            PlatformValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(platformDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Platform platform = new()
            {
                Id = Guid.NewGuid(),
                Name = platformDto.Name
            };

            await unitOfWork.PlatformRepository.CreateAsync(platform);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Platform platform = await unitOfWork.PlatformRepository.GetByIdAsync(id)
                ?? throw new InvalidDataException("There's no paltform in the database");

            await unitOfWork.PlatformRepository.DeleteAsync(id);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<List<string>> FindByNameAsync(string platformName)
        {
            List<string> platformNames = await unitOfWork.PlatformRepository.FindManyByNameAsync(platformName)
                ?? throw new InvalidDataException($"There's no platforms with name: {platformName}");

            return platformNames;
        }
    }
}