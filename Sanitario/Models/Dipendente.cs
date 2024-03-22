using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class Dipendente
    {
        [Key]
        public int IdDipendente { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Ruolo { get; set; }
    }
}
