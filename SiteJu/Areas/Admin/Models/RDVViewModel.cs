using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace SiteJu.Areas.Admin.Models
{
    public class RDVViewModel
    {
        public int Id { get; set; }
        [DisplayName("Heure de rdv")]
        public DateTime At { get; set; }
        [DisplayName("Client")]
        public int ClientId { get; set; }

        public ClientViewModel Client { get; set; }
        public List<PrestationViewModel> Prestation { get; set; }

    }
}
