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
            new Client{Firstname="Pauline",Lastname="Lopez",Telephone="0670809090",Email="popo@hotmail.fr"},
            new Client{Firstname="Olivier",Lastname="Houssin",Telephone="0690809090",Email="olive@hotmail.fr"},

            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();
        }
    }
}