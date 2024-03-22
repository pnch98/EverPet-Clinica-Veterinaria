using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Cassetto
    {
        [Key]
        public int IdCassetto { get; set; }

        [ForeignKey("Armadietto")]
        public int IdArmadietto { get; set; }
        [Required]
        public int NumeroCassetto { get; set; }
        public virtual Armadietto Armadietto { get; set; }
        public virtual ICollection<Prodotto> Prodotti { get; set; }
    }
}
