using Microsoft.EntityFrameworkCore;
using Sanitario.Models;

namespace Sanitario.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Animale> Animali { get; set; }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Dipendente> Dipendenti { get; set; }
        public DbSet<Prodotto> Prodotti { get; set; }
        public DbSet<AnimaleSmarrito> AnimaliSmarriti { get; set; }
        public DbSet<Armadietto> Armadietti { get; set; }
        public DbSet<Cassetto> Cassetti { get; set; }
        public DbSet<CuraPrescritta> CurePrescritte { get; set; }
        public DbSet<Visita> Visite { get; set; }
        public DbSet<Vendita> Vendite { get; set; }
        public DbSet<DettagliVendita> DettagliVendite { get; set; }
    }
}
