using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    public class EditQuestionDTO
    {
        public string? Label { get; set; }
        public int? CategoryId { get; set; }
        public List<EditAnswerDTO>? Options { get; set; }
        public int? LinkedAnswerId { get; set; }
        public string? ExpectedTextAnswer { get; set; }
        public bool? ExpectedTrueFalse { get; set; }
    }
}