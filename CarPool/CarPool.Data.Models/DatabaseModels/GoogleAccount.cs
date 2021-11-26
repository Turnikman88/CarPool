using CarPool.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Data.Models.DatabaseModels
{
    public class GoogleAccount : BaseModel<Guid>
    {
        public string Email { get; set; }
    }
}
