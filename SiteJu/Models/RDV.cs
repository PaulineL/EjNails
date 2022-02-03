using System;
namespace SiteJu.Models
{
	public class RDV
	{

        public int Id { get; set; }
        public DateTime At { get; set; }
        public int ClientId { get; set; }
        public int PrestationId { get; set; }

        public Client Client { get; set; }
        public Prestation Prestation { get; set; }
    }
}

