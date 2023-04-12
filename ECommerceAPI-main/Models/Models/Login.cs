using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class Login
	{
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(16)]
        public string Password { get; set; }
	}
}

