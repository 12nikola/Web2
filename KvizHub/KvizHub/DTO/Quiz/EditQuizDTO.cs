
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;    
using KvizHub.Enums;

namespace KvizHub.DTO.Quiz
{
    public class EditQuizDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string? TitleLabel { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string? DescriptionLabel { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public Difficulty difficulty { get; set; }

        [Required]
        public TimeSpan? MaxDuration { get; set; }
        public bool? IsPublished { get; set; }
    }
}