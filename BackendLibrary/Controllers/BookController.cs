using BackendLibrary.Dtos.Book;
using BackendLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(IBookService bookService) : ControllerBase
    {
        private readonly IBookService _bookService = bookService;

        [HttpPost]
        public async Task<ActionResult<BookResponse>> InsertCategory(CreateBookRequest bookDto)
        {
            var newBookResponse = await _bookService.InsertBookAsync(bookDto);

            return CreatedAtAction(nameof(FindBookById),
                new { id = newBookResponse.Id },
                newBookResponse
            );
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResponse>>> FindBooks([FromQuery] string? title)
        {
            var books = await _bookService.FindBooksAsync(title);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponse>> FindBookById(int id)
        {
            var book = await _bookService.FindBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookResponse>> UpdateCategory(int id, UpdateBookRequest bookDto)
        {
            var updatedBook = await _bookService.UpdateBookAsync(id, bookDto);

            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _bookService.DeleteBookAsync(id);

            return NoContent();
        }
    }
}