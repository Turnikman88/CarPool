using CarPool.Data.Common.Models;
using System.Collections.Generic;

namespace CarPool.Data.Models.DatabaseModels
{
    public class City : BaseDeletableModel<int>
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
