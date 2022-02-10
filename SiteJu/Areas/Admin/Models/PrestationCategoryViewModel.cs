using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiteJu.Areas.Admin.Models
{
    public class PrestationCategoryViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nom")]
        public string Name { get; set; }
    }
}

