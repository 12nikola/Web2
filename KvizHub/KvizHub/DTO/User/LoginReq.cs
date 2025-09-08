<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.User
{
    public class LoginReq
    {
        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
=======
﻿using System.ComponentModel.DataAnnotations;

namespace KvizHub.DTO.User
{
    public class LoginReq
    {
        [Required]
        [EmailAddress]
        [MinLength(5)]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
>>>>>>> 54cd738791c79d9ca97c296ee5be4d0d2d95cb2b
