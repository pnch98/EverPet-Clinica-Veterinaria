using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Animale
    {
        [Key]
        public int IdAnimale { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Tipologia { get; set; }
        public DateOnly DataRegistrazione { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        public DateOnly DataNascita { get; set; }
        [Required]
        public string ColoreMantello { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }
        public string Microchip { get; set; } = "";
        [NotMapped]
        public string NomeCompleto
        {
            get
            {
                return $"{Nome} ({Cliente?.CodiceFiscale})";
            }
        }
        public virtual ICollection<Visita> Visite { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
