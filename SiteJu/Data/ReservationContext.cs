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
        public DbSet<PrestationOption> PrestationOptions { get; set; }

        // converti model C# en model BDD
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RDV>()
                .HasMany(p => p.Prestations)
                .WithMany(r => r.RDVS)
                .UsingEntity(rp => rp.ToTable("RDVPrestations"));

            modelBuilder.Entity<PrestationOption>()
                .HasMany(p => p.CompatibleWith)
                .WithMany(po => po.OptionsAvailable)
                .UsingEntity(rp => rp.ToTable("PrestationOptionLink"));

            modelBuilder.Entity<PrestationOption>()
                .HasMany(r => r.RDVS)
                .WithMany(po => po.Options)
                .UsingEntity(rp => rp.ToTable("RDVPrestationsOptions"));


            modelBuilder.Entity<Prestation>().ToTable("Prestations");
            modelBuilder.Entity<PrestationOption>().ToTable("PrestationsOptions");
        }

    }
}


