<<<<<<< HEAD
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
=======
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
>>>>>>> 54cd738791c79d9ca97c296ee5be4d0d2d95cb2b
