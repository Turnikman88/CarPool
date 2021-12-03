using CarPool.Data.Common.Models;
using System;

namespace CarPool.Data.Models.DatabaseModels
{
    public class GoogleAccount : BaseModel<Guid>
    {
        public string Email { get; set; }
    }
}
