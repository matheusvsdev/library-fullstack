using System.ComponentModel.DataAnnotations;

namespace BackendLibrary.Dtos.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}