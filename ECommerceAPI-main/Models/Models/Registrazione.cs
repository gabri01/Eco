using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Registrazione
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(16)]
        public string Password { get; set; }
    }
}

