using System.ComponentModel.DataAnnotations;

namespace BackendLibrary.Dtos.Category
{
    public class UpdateCategoryRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}