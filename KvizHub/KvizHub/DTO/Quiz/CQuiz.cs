<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KvizHub.Enums;
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    public class CQuiz
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int TimeLimit { get; set; }
        [Required]
        [MinLength(1)]
        public List<CQuestion> Questions { get; set; }

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KvizHub.Enums;
using KvizHub.DTO;

namespace KvizHub.DTO.Quiz
{
    public class CQuiz
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int TimeLimit { get; set; }
        [Required]
        [MinLength(1)]
        public List<CQuestion> Questions { get; set; }

    }
}
>>>>>>> 54cd738791c79d9ca97c296ee5be4d0d2d95cb2b
