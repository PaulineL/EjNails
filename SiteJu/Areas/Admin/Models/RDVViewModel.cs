using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteJu.Areas.Admin.Models
{
    public class RDVViewModel
    {
        public int Id { get; set; }
        [DisplayName("Heure de rdv")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime At { get; set; }
        [DisplayName("Client")]

        public int ClientId { get; set; }

        public ClientViewModel Client { get; set; }
        public List<PrestationViewModel> Prestation { get; set; }

    }
}
