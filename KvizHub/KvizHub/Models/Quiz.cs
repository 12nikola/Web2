
﻿namespace KvizHub.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int NumberOfQuestions { get; set; }
        public int TimeLimit { get; set; } 
        public string? Difficulty { get; set; }
        public string? CreatorUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
