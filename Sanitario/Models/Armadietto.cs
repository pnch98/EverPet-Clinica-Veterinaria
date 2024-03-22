using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class Armadietto
    {
        [Key]
        public int IdArmadietto { get; set; }
        [Required]
        public int NumeroArmadietto { get; set; }
        public virtual ICollection<Cassetto> Cassetti { get; set; }
    }
}
