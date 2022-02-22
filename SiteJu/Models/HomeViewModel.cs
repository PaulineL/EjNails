using System;
using System.Collections.Generic;
using SiteJu.Areas.Admin.Models;

namespace SiteJu.Models
{
	public class HomeViewModel
	{
		public string ProfilPicture { get; set; }
		public Contact Contact { get; set; }

        public List<PrestationViewModel> Prestations { get; set; }
    }
}

