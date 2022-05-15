using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SiteJu.Data
{
    public class Client : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Note { get; set; }

        public IEnumerable<RDV> RDV { get; set; }

    }
}

