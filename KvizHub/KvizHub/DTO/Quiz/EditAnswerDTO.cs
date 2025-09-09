
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class EditAnswerDTO
    {
        public int OptionKey { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string? ResponseLabel { get; set; }

        [Required]
        public bool? Correct { get; set; }
    }
}
