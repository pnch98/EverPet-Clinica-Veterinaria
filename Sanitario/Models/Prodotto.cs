using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Prodotto
    {
        [Key]
        public int IdProdotto { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descrizione { get; set; }
        [Required]
        public double Prezzo { get; set; }
        [Required]
        [Display(Name = "Tipo Prodotto")]
        public string TipoProdotto { get; set; }
        [ForeignKey("Cassetto")]
        [Display(Name = "Cassetto")]
        public int IdCassetto { get; set; }
        [NotMapped]
        public string NomeCompleto
        {
            get
            {
                return $"{Nome} - {Prezzo}€";
            }
        }


        public virtual Cassetto Cassetto { get; set; }
        public virtual ICollection<CuraPrescritta> CurePrescritte { get; set; }
        public virtual ICollection<DettagliVendita> DettagliVendite { get; set; }

    }
}
