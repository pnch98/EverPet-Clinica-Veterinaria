using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class CuraPrescritta
    {
        [Key]
        public int IdCuraPrescritta { get; set; }
        [ForeignKey("Visita")]
        [Required]
        public int IdVisita { get; set; }
        [ForeignKey("Prodotto")]
        [Required]
        public int IdProdotto { get; set; }

        public virtual Visita Visita { get; set; }
        public virtual Prodotto Prodotto { get; set; }
    }
}
