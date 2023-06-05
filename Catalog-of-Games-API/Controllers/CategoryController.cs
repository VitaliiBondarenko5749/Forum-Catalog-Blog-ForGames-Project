using Catalog_of_Games_BAL.Contracts;
using Catalog_of_Games_BAL.DTOs;
using Catalog_of_Games_BAL.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Catalog_of_Games_API.Controllers
{
    [Route("/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> logger;
        private readonly ICategoryService categoryService;

        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            this.logger = logger;
            this.categoryService = categoryService;
        }

        // FindCategoryByName
        [HttpGet("find-by-name")]
        [Authorize] 
        public async Task<ActionResult<List<string>>> FindCategoryByNameAsync(string categoryName)
        {
            try
            {
                List<string> categoryNames = await categoryService.FindByNameAsync(categoryName);

                return Ok(categoryNames);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet] // Get all categories
        public async Task<ActionResult<List<CategoryInfoDto>>> GetAllCategoriesAsync()
        {
            try
            {
                List<CategoryInfoDto> categories = await categoryService.GetAllCategoriesAsync();

                if (categories is null)
                {
                    return NotFound();
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("add-category")] // Add new category
        [Authorize]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryInsertDto categoryDto)
        {
            try
            {
                await categoryService.AddAsync(categoryDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("update-category/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategoryAsync(Guid id, CategoryInsertDto categoryInsertDto)
        {
            try
            {
                await categoryService.UpdateAsync(id, categoryInsertDto);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete-category/{id}")] // Delete category
        [Authorize]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            try
            {
                await categoryService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError($"Error: {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}