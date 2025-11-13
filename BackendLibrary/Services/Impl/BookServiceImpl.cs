using AutoMapper;
using AutoMapper.QueryableExtensions;
using BackendLibrary.DataContext;
using BackendLibrary.Dtos.Book;
using BackendLibrary.Models;
using BackendLibrary.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackendLibrary.Services.Impl
{
    public class BookServiceImpl(LibraryDbContext context, IMapper mapper) : IBookService
    {
        private readonly LibraryDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<BookResponse> InsertBookAsync(CreateBookRequest bookDto)
        {
            var bookCategories = await _context.Categories
                .Where(c => bookDto.CategoryIds.Contains(c.Id))
                .ToListAsync();

            if (bookCategories.Count != bookDto.CategoryIds.Count)
            {
                throw new NotFoundException("One or more of the categories listed do not exist.");
            }

            if (bookDto.CategoryIds.Count > 3)
            {
                throw new InvalidArgumentException("Um livro não pode ter mais de 3 categorias.");
            }

            var newBook = _mapper.Map<Book>(bookDto);

            newBook.Categories = bookCategories;

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookResponse>(newBook);
        }

        public async Task<IEnumerable<BookResponse>> FindBooksAsync(string? title)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(l => l.Title.Contains(title));
            }

            return await query
                .ProjectTo<BookResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<BookResponse?> FindBookByIdAsync(int id)
        {
            var existingBook = await _context.Books
                .Where(b => b.Id == id)
                .ProjectTo<BookResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync() ??
                    throw new NotFoundException($"Book not found.");

            return existingBook;
        }

        public async Task<BookResponse> UpdateBookAsync(int id, UpdateBookRequest bookDto)
        {

            var bookToUpdate = await _context.Books
                .Include(b => b.Categories)
                .FirstOrDefaultAsync(b => b.Id == id) ??
                    throw new NotFoundException($"Book not found.");

            _mapper.Map(bookDto, bookToUpdate);

            if (bookDto.CategoryIds != null)
            {
                var updateCategories = await _context.Categories
                    .Where(c => bookDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();

                if (updateCategories.Count != bookDto.CategoryIds.Count)
                {
                    throw new NotFoundException("One or more of the categories listed do not exist.");
                }

                if (bookDto.CategoryIds.Count > 3)
                {
                    throw new InvalidArgumentException("Um livro não pode ter mais de 3 categorias.");
                }

                bookToUpdate.Categories = updateCategories;
            }
            
            await _context.SaveChangesAsync();
            return _mapper.Map<BookResponse>(bookToUpdate);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id) ??
                throw new NotFoundException($"Book not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}