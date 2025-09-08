using KvizHub.Enums;
using System;
using System.Collections.Generic;

namespace KvizHub.DTO.Quiz
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int NumberOfQuestions { get; set; }
        public int TimeLimit { get; set; } 
        public string CreatorUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<int> QuestionIds { get; set; }
    }
}
