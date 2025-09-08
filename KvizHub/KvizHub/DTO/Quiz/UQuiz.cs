using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    
using KvizHub.Enums;

namespace KvizHub.DTO.Quiz
{
    public class UQuiz
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Category { get; set; }

        [Required]
        public int TimeLimit { get; set; }
        public bool IsPublished { get; set; }
    }
}
