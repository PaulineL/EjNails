﻿using System;
using System.Collections.Generic;

namespace SiteJu.Models
{
	public class Client
	{
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Telephone { get; set; }
        public string Email{ get; set; }

        public IEnumerable<RDV> RDV { get; set; }

    }
}

