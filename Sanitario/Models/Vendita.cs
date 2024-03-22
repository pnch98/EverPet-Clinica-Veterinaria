using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Vendita
    {
        [Key]
        public int IdVendita { get; set; }
        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public DateTime DataVendita { get; set; } = DateTime.Now;

        public double PrezzoTotale { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<DettagliVendita> DettagliVendite { get; set; }
    }
}
