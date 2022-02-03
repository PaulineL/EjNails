using System;
namespace SiteJu.Models
{
	public class RDV
	{

        public int ID { get; set; }
        public string Heure { get; set; }
        public string Date { get; set; }
        public string Firstname { get; set; }
        public string Name_prestaitons { get; set; }

        public Client Client { get; set; }
        public Prestation Prestation { get; set; }


    }
}

