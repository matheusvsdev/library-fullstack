using System.ComponentModel.DataAnnotations;

namespace BackendLibrary.Dtos.Book
{
    public class UpdateBookRequest
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Author { get; set; } = null!;

        [Required]
        public List<int> CategoryIds { get; set; } = [];
    }
}