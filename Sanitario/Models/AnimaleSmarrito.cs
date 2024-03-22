using System.ComponentModel.DataAnnotations;

namespace Sanitario.Models
{
    public class AnimaleSmarrito
    {

        [Key]
        public int IdAnimaleSmarrito { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Tipologia { get; set; }
        [Display(Name = "Data di registrazione")]
        public DateOnly DataRegistrazione { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        [Display(Name = "Data di nascita")]
        public DateOnly DataNascita { get; set; }
        [Required]
        [Display(Name = "Colore mantello")]
        public string ColoreMantello { get; set; }
        [Display(Name = "Codice fiscale proprietario")]
        public string CodiceFiscaleProprietario { get; set; } = "";
        public string Microchip { get; set; } = "";
        [Display(Name = "Data di inizio ricovero")]
        public DateOnly DataInizioRicovero { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public string Foto { get; set; }
    }
}
