using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
{
    public class Country : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
