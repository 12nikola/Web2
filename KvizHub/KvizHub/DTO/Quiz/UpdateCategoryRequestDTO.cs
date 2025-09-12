using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class UpdateCategoryRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string? CategoryName { get; set; }
    }
}
