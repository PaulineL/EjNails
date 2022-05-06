using System;
using System.ComponentModel;


namespace SiteJu.Areas.Admin.Models
{
    public class ClientViewModel
    {
        public int ID { get; set; }
        [DisplayName("Nom")]
        public string Firstname { get; set; }
        [DisplayName("Prénom")]
        public string Lastname { get; set; }
        [DisplayName("Téléphone")]
        public string Telephone { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Note")]
        public string Information { get; set; }

    }
}
