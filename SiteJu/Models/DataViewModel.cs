using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteJu.Models
{
    public class DataViewModel
    {
        public string Name { get; set; }
        [DisplayName("Prénom")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public List<string> Passion { get; set; }
        public string Photo { get; set; }
    }
}
