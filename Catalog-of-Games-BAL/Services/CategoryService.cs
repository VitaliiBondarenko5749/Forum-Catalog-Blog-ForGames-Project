using AutoMapper;
using FluentValidation.Results;
using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Validators;
using Catalog_of_Games_DAL.Entities;
using Catalog_of_Games_DAL.Repositories.Contracts;
using System.Text;

namespace Catalog_of_Games_BAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Отримуємо всі категорії по списку.
        public async Task<List<CategoryInfoDto>> GetAllCategoriesAsync()
        {
            List<Category> categories = await unitOfWork.CategoryRepository.GetAllAsync(pageNumber: 0, pageSize: 0);

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.CreateMap<Category, CategoryInfoDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            List<CategoryInfoDto> categoryDtos = mapper.Map<List<CategoryInfoDto>>(categories);

            return categoryDtos;
        }

        // Додаємо нову категорію
        public async Task AddAsync(CategoryInsertDto categoryDto)
        {
            CategoryValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(categoryDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Category category = new()
            {
                Id = Guid.NewGuid(),
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                Icon = string.Empty,
            };

            await unitOfWork.CategoryRepository.CreateAsync(category);

            await unitOfWork.SaveChangesAsync();    
        }

        // Оновлюємо інформацію в категорії
        public async Task UpdateAsync(Guid id, CategoryInsertDto categoryDto)
        {
            CategoryValidator validator = new();
            ValidationResult result = await validator.ValidateAsync(categoryDto);

            if (!result.IsValid)
            {
                StringBuilder stringBuilder = new();

                foreach (ValidationFailure error in result.Errors)
                {
                    stringBuilder.AppendLine(error.ErrorMessage);
                }

                throw new InvalidDataException(stringBuilder.ToString());
            }

            Category category = await unitOfWork.CategoryRepository.GetByIdAsync(id) ??
                throw new NullReferenceException("There's no such category in the database");

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;

            await unitOfWork.CategoryRepository.UpdateAsync(category);

            await unitOfWork.SaveChangesAsync();
        }

        // Видаляємо існуючу категорію
        public async Task DeleteAsync(Guid id)
        {
            Category? category = await unitOfWork.CategoryRepository.GetByIdAsync(id) ?? 
                throw new NullReferenceException("There's no such category in the database");

            await unitOfWork.CategoryRepository.DeleteAsync(id);

            await unitOfWork.SaveChangesAsync();
        }

        // Знаходимо категорії за іменем
        public async Task<List<string>> FindByNameAsync(string categoryName)
        {
            List<string> categoryNames = await unitOfWork.CategoryRepository.FindManyByNameAsync(categoryName)
                ?? throw new NullReferenceException($"There's no such categories with name: {categoryName}");

            return categoryNames;
        }
    }
}