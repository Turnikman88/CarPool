using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models
{
    class Trips : BaseDeletableModel<Guid>
    {
        public Trips()
        {
            
        }

        public string StartAddress { get; set; }
    }
}
