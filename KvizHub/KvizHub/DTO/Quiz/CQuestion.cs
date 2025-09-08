using KvizHub.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    
    public class CQuestion
    {
        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Text { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public List<CAnswer> Answers { get; set; }
        [Required]
        public int Points { get; set; }
    }
}
