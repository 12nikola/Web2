using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class UpdateCategoryRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? CategoryName { get; set; }
    }
}
