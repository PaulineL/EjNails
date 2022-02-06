using System.Collections.Generic;

namespace SiteJu.Data
{
    public class PrestationOption : Prestation
    {
        public IEnumerable<Prestation> CompatibleWith { get; set; }
        public bool Quantifiable { get; set; }
        public int? MaxAvailable { get; set; }
    }
}
