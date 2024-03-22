using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        public string CodiceFiscale { get; set; }
        [NotMapped]
        public string NomeCompleto
        {
            get
            {
                return $"{Nome} {Cognome} ({CodiceFiscale})";
            }
        }
        public virtual ICollection<Animale> Animali { get; set; }
        public virtual ICollection<Vendita> Vendite { get; set; }
    }
}
