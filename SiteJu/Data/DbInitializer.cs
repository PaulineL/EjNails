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


            context.Prestations.AddRange(prestations);
            context.PrestationOptions.AddRange(prestationOptions);

            context.SaveChanges();
        }
    }
}