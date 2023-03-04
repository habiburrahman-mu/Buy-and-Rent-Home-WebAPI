using System;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI.Data.Entities
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int PropertyId { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedBy { get; set; }

        public virtual User LastUpdatedByNavigation { get; set; }
        public virtual Property Property { get; set; }
    }
}
