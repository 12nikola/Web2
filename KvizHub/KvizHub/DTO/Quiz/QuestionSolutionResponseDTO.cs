using KvizHub.Enums;

namespace KvizHub.DTO.Quiz
{
    public class QuestionSolutionResponseDTO
    {
        public int SolutionId { get; set; }

        public int QuestionId { get; set; }

        public QuestionType Type { get; set; }

        public string? Category { get; set; }

        public string? Text { get; set; }

        public List<BasicAnswerDTO>? PossibleAnswers { get; set; }

        public List<string>? SelectedAnswers { get; set; }
    }
}
