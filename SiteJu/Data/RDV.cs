using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SiteJu.Data
{
	public class RDV
	{

        public int Id { get; set; }
        public DateTime At { get; set; }
        public int ClientId { get; set; }

        public Client Client { get; set; }

        public ICollection<Prestation> Prestations { get; set; }
        public ICollection<PrestationOption> Options { get; set; }
    }
}

