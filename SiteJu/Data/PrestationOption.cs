using System;
using System.Collections.Generic;

namespace SiteJu.Data
{
    public class PrestationOption
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int AdditionalPrice { get; set; }
        public TimeSpan AdditionalTime { get; set; }
        public bool Quantifiable { get; set; }
        public int? MaxAvailable { get; set; }

        public IEnumerable<Prestation> CompatibleWith { get; set; }
        public IEnumerable<RDV> RDVS { get; set; }
    }
}
