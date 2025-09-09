
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.Quiz
{
    public class NewAnswerDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string? Label { get; set; }
        [Required]
        public bool Valid { get; set; }
    }
}
