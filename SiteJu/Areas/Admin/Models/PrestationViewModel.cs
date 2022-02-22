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
        [DisplayName("Catégorie")]

        // Fait le lient entre une prestation ET sa categorie
        public PrestationCategoryViewModel Category { get; set; }

        // Permets a la vue de lister TOUTES les categories disponible
        // afin que l'utilisateurs puissent en associer une avec la prestation
        // qu'il veut modifier ou creer
        public List<PrestationCategoryViewModel> CategoryAvailable { get; set; }

        public List<PrestationOptionViewModel> Options { get; set; }
    }

}

