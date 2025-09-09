using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KvizHub.Enums;
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    public class NewQuizDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string? Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public Difficulty difficulty { get; set; }
        [Required]
        public TimeSpan Limit { get; set; }
        [Required]
        [MinLength(1)]
        public List<NewQuestionDTO>? Questions { get; set; }

    }
}
