using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    public class UQuestion
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(300)]
        public string Text { get; set; }
        [Required]
        public List<UAnswer> Answers { get; set; }
        [Required]
        public int Points { get; set; }
    }
}