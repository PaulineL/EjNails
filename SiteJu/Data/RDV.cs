using System;
using System.Collections.Generic;

namespace SiteJu.Data
{
	public class RDV
	{

        public int Id { get; set; }
        public DateTime At { get; set; }
        public int ClientId { get; set; }

        public Client Client { get; set; }
        public IEnumerable<Prestation> Prestations { get; set; }

        public IEnumerable<PrestationOption> Options { get; set; }
    }
}

