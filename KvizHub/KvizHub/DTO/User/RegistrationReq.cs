
﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace KvizHub.DTO.User
{
    public class RegistrationReq
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores.")]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public string Image { get; set; }
    }
}