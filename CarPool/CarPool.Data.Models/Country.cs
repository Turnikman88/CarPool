using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
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
