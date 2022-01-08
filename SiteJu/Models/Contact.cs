using System;
using System.ComponentModel;

namespace SiteJu.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [DisplayName("Prénom")]
        public string Name { get; set; }
        [DisplayName("Nom")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }         
    }
}