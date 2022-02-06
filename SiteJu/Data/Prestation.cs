using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SiteJu.Data
{
    public class Prestation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public TimeSpan Duration { get; set; }
        public PrestationCategory Category { get; set; }

        public IEnumerable<RDV> RDVS { get; set; }
        public IEnumerable<PrestationOption> OptionsAvailable { get; set; }
    }
}

