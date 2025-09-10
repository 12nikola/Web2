using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class QuestionAnswerDTO
    {
        [Required]
        public int QuestionId { get; set; }
        
        public bool? UserAnswerBoolean { get; set; }
        public string? UserAnswerLabel { get; set; }
        [MaxLength(300)]
        public List<string>? UserAnswerOptions { get; set; }
    }
}
