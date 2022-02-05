
using System;
using System.ComponentModel;


namespace SiteJu.Models
{
	public class PrestationViewModel
	{
        public int Id { get; set; }
        [DisplayName("Nom")]
        public string Name { get; set; }
        [DisplayName("Durée")]
        public int Duration { get; set; }
        [DisplayName("Prix")]
        public int Price { get; set; }
    }
}

