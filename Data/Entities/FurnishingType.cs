using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities
{
    public partial class FurnishingType
    {
        public FurnishingType()
        {
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}
