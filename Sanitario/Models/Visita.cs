using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sanitario.Models
{
    public class Visita
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Animale")]
        public int IdAnimale { get; set; }

        public DateOnly DataVisita { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        public string Esame { get; set; }
        public bool IsArchiviato { get; set; } = false;

        public virtual Animale Animale { get; set; }
        public virtual ICollection<CuraPrescritta> CurePrescritte { get; set; }
    }
}
