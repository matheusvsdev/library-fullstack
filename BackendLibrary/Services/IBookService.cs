using BackendLibrary.Dtos.Book;

namespace BackendLibrary.Services
{
    public interface IBookService
    {
        Task<BookResponse> InsertBookAsync(CreateBookRequest bookDto);
        Task<IEnumerable<BookResponse>> FindBooksAsync(string? title);
        Task<BookResponse?> FindBookByIdAsync(int id);
        Task<BookResponse> UpdateBookAsync(int id, UpdateBookRequest bookDto);
        Task DeleteBookAsync(int id);
    }
}