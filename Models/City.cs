﻿using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Models
{
    public partial class City
    {
        public City()
        {
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
