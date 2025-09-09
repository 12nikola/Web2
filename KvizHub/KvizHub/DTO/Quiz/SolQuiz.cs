using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class SolQuiz
    {
        [Required]
        public TimeSpan MaxDuration { get; set; }

        [Required]
        public List<SolQuestion>? solves { get; set; }

    }
}
