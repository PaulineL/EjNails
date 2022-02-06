using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace SiteJu.Areas.Admin.Models
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
        public bool IsSelected { get; set; }

        public PrestationCategoryViewModel Category { get; set; }

        public List<PrestationOptionViewModel> Options { get; set; }
    }

    public class PrestationCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

