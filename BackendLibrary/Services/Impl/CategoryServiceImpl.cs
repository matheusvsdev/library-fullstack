using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackendLibrary.DataContext;
using BackendLibrary.Dtos.Category;
using BackendLibrary.Models;
using BackendLibrary.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackendLibrary.Services.Impl
{
    public class CategoryServiceImpl(LibraryDbContext context, IMapper mapper) : ICategoryService
    {
        private readonly LibraryDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryResponse> InsertCategoryAsync(CreateCategoryRequest categoryDto)
        {
            var newCategory = _mapper.Map<Category>(categoryDto);

            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(newCategory);
        }

        public async Task<IEnumerable<CategoryResponse>> FindCategoriesAsync(string? name)
        {
            var query = _context.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(l => l.Name.Contains(name));
            }

            return await query
                .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<CategoryResponse?> FindCategoryByIdAsync(int id)
        {
            var existingCategory = await _context.Categories
                .Where(c => c.Id == id)
                .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ??
                    throw new NotFoundException($"Category not found.");

            return existingCategory;
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(int id, UpdateCategoryRequest categoryDto)
        {
            var categoryToUpdate = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id) ??
                    throw new NotFoundException($"Category not found."); 

            _mapper.Map(categoryDto, categoryToUpdate);

            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryResponse>(categoryToUpdate);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id) ??
                throw new NotFoundException($"Category not found.");

            var hasBooks = await _context.Books
                .AnyAsync(b => b.Categories.Any(c => c.Id == id));

            if (hasBooks)
            {
                throw new InvalidOperationException("It is not possible to delete a category that contains books.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}