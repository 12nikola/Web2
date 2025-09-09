
﻿using KvizHub.Enums;
using System;
using System.Collections.Generic;

namespace KvizHub.DTO.Quiz
{
    public class QuizDTO
    {
        public int QuizId { get; set; }
        public string? TitleLabel { get; set; }
        public string? DescriptionLabel { get; set; }
        public Difficulty Difficulty { get; set; }
        public bool Editable { get; set; }
        public TimeSpan MaxDuration { get; set; } 
        public bool? isActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<int>? QuestionKeys { get; set; }
    }
}