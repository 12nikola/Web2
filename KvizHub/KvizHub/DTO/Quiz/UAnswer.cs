
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class UAnswer
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Text { get; set; }

        [Required]
        public bool IsSelected { get; set; }
    }
}
