using CarPool.Data.Common.Models;
using System.Collections.Generic;

namespace CarPool.Data.Models.DatabaseModels
{
    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
