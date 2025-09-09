
﻿using KvizHub.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    
    public class NewQuestionDTO
    {
        [Required]
        [EnumDataType(typeof(QuestionType))]
        public QuestionType Type { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        public string? Label { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [MinLength(2)]
        [MaxLength(10)]
        public List<NewAnswerDTO>? Choices { get; set; }
    
        public string? ExpectedTextChoices { get; set; }
        public bool? ExpectedTrueFalse { get; set; }
    }
}