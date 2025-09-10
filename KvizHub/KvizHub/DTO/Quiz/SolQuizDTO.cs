using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class SolQuizDTO
    {
        [Required]
        public TimeSpan MaxDuration { get; set; }

        [Required]
        public List<SolQuestionDTO>? solves { get; set; }

    }
}
