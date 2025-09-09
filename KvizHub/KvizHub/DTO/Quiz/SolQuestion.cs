using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class SolQuestion
    {
        [Required]
        public int QuestionId { get; set; }

        public bool? BooleanLabel { get; set; }

        public string? SingleLabel { get; set; }

        [MaxLength(4)]
        public List<string>? MultipleLabels { get; set; }
    }
}
