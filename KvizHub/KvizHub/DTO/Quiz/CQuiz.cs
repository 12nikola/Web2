using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KvizHub.Enums;
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    public class CQuiz
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int TimeLimit { get; set; }
        [Required]
        [MinLength(1)]
        public List<CQuestion> Questions { get; set; }

    }
}
