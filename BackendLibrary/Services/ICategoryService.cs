using BackendLibrary.Dtos.Category;

namespace BackendLibrary.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponse> InsertCategoryAsync(CreateCategoryRequest categoryDto);
        Task<IEnumerable<CategoryResponse>> FindCategoriesAsync(string? name);
        Task<CategoryResponse?> FindCategoryByIdAsync(int id);
        Task<CategoryResponse> UpdateCategoryAsync(int id, UpdateCategoryRequest categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}