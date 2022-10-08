using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public virtual User LastUpdatedByNavigation { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
