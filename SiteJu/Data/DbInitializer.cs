using SiteJu.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SiteJu.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReservationContext context)
        {
            if (!context.Database.EnsureCreated())
            {
                return;
            }

            var clients = new Client[]
            {
                new Client{ID = 1, Firstname="Pauline",Lastname="Lopez",Telephone="0670809090",Email="popo@hotmail.fr"},
                new Client{ID = 2, Firstname="Olivier",Lastname="Houssin",Telephone="0690809090",Email="olive@hotmail.fr"}
            };

            var prestationCategory = new[]
            {
                new PrestationCategory
                {
                    Id = 1,
                    Name = "Gel"
                },
                new PrestationCategory
                {
                    Id = 2,
                    Name = "Vernis Semi-permanent"
                },
            };

            var prestations = new[]
            {
                new Prestation
                {
                    ID = 1,
                    Name = "Pose complète capsules gel",
                    Price = 45,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[0]
                },
                new Prestation
                {
                    ID = 2,
                    Name = "Gel sur ongles naturels",
                    Price = 40,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[0]
                },
                new Prestation
                {
                    ID = 3,
                    Name = "Remplissage",
                    Price = 40,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[0]
                },
                new Prestation
                {
                    ID = 4,
                    Name = "Gel sur ongles naturels",
                    Price = 40,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[0]
                },
                new Prestation
                {
                    ID = 5,
                    Name = "Gel sur ongles naturels",
                    Price = 40,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[0]
                },
                new Prestation
                {
                    ID = 6,
                    Name = "Vernis semi-permanent",
                    Price = 40,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[1]
                },
                new Prestation
                {
                    ID = 7,
                    Name = "Dépose",
                    Price = 40,
                    Duration = TimeSpan.FromHours(1),
                    Category = prestationCategory[1]
                }
            };

            var prestationOptions = new[]
            {
                new PrestationOption
                {
                    ID = 8,
                    Name = "Pose XL",
                    AdditionalTime = TimeSpan.Zero,
                    CompatibleWith = new List<Prestation>() { prestations[1] },
                    Quantifiable = false,
                    AdditionalPrice = 5
                },
                new PrestationOption
                {
                    ID = 9,
                    Name = "Nail Art",
                    AdditionalTime = TimeSpan.FromMinutes(30),
                    CompatibleWith = new List<Prestation>() { prestations[1], prestations[0] },
                    Quantifiable = false,
                    AdditionalPrice = 5
                },
                new PrestationOption
                {
                    ID = 10,
                    Name = "Réparation d'un ongle cassé",
                    AdditionalTime = TimeSpan.FromMinutes(30),
                    CompatibleWith = new List<Prestation>() { prestations[1] },
                    Quantifiable = false,
                    AdditionalPrice = 5,
                    MaxAvailable = 3
                }
            };

            var rdvs = new[]
            {
                new RDV
                {
                    Id = 1,
                    ClientId = 1,
                    Prestations = new List<Prestation>() { prestations[0] },
                    At = DateTime.Now.AddDays(1)
                },
                new RDV
                {
                    Id = 2,
                    ClientId = 2,
                    Prestations = new List<Prestation>() { prestations[1] },
                    At = DateTime.Now.AddDays(2).AddHours(-4)
                },
                new RDV
                {
                    Id = 3,
                    ClientId = 2,
                    Prestations = new List<Prestation>() { prestations[0], prestations[1] },
                    At = DateTime.Now.AddHours(1)
                },
            };

            context.Clients.AddRange(clients);
            context.Prestations.AddRange(prestations);
            context.PrestationOptions.AddRange(prestationOptions);
            context.RDVS.AddRange(rdvs);

            context.SaveChanges();
        }
    }
}