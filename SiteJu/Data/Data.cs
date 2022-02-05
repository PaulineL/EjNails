using SiteJu.Models;
using System;
using System.Linq;

namespace SiteJu.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ReservationContext context)
        {
            context.Database.EnsureCreated();

            var clients = new Client[]
            {
                new Client{ID = 1, Firstname="Pauline",Lastname="Lopez",Telephone="0670809090",Email="popo@hotmail.fr"},
                new Client{ID = 2, Firstname="Olivier",Lastname="Houssin",Telephone="0690809090",Email="olive@hotmail.fr"},

            };

            var prestations = new[]
            {
                new Prestation
                {
                    ID = 1,
                    Prestations = "Ongles de pouff",
                    Price = 10,
                    Duration = TimeSpan.FromHours(1)
                },
                new Prestation
                {
                    ID = 2,
                    Prestations = "Ongles de luxe",
                    Price = 100,
                    Duration = TimeSpan.FromHours(2)
                }
            };

            var rdvs = new[]
            {
                new RDV
                {
                    Id = 1,
                    ClientId = 1,
                    PrestationId = 1,
                    At = DateTime.Now.AddDays(4)
                },
                new RDV
                {
                    Id = 2,
                    ClientId = 2,
                    PrestationId = 2,
                    At = DateTime.Now.AddDays(2).AddHours(-4)
                },
                new RDV
                {
                    Id = 3,
                    ClientId = 2,
                    PrestationId = 2,
                    At = DateTime.Now.AddHours(1)
                },
            };

            context.Clients.AddRange(clients);
            context.Prestations.AddRange(prestations);
            context.RDVS.AddRange(rdvs);

            context.SaveChanges();
        }
    }
}