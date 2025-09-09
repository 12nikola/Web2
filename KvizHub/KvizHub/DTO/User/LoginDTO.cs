
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.User
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string? Password { get; set; }
    }
}