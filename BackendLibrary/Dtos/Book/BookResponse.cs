using BackendLibrary.Dtos.Category;

namespace BackendLibrary.Dtos.Book
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public List<CategoryResponse> Categories { get; set; } = [];
    }
}