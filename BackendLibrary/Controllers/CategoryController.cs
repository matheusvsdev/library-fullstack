using BackendLibrary.Dtos.Category;
using BackendLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> InsertCategory(CreateCategoryRequest categoryDto)
        {
            var newCategoryResponse = await _categoryService.InsertCategoryAsync(categoryDto);

            return CreatedAtAction(nameof(FindCategoryById),
                new { id = newCategoryResponse.Id },
                newCategoryResponse
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> FindCategories([FromQuery] string? name)
        {
            var categories = await _categoryService.FindCategoriesAsync(name);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> FindCategoryById(int id)
        {
            var category = await _categoryService.FindCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory(int id, UpdateCategoryRequest categoryDto)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}