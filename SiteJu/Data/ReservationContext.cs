using System;
using Microsoft.EntityFrameworkCore;

namespace SiteJu.Data
{
	public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
        }

    // Le code crée une DbSet propriété pour chaque jeu d’entités.
    // Dans la terminologie EF :
    // Un jeu d’entités correspond généralement à une table de base de données.
    // Une entité correspond à une ligne dans la table.

        public DbSet<Client> Clients { get; set; }
        public DbSet<RDV> RDVS { get; set; }
        public DbSet<Prestation> Prestations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}


