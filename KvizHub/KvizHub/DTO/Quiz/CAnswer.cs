
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class CAnswer
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Text { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
    }
}
