using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class DettagliVendita
    {
        [Key]
        public int IdDettagliVendita { get; set; }
        [ForeignKey("Vendita")]
        public int IdVendita { get; set; }
        [ForeignKey("Prodotto")]
        public int IdProdotto { get; set; }


        public virtual Vendita Vendita { get; set; }
        public virtual Prodotto Prodotto { get; set; }
    }
}
