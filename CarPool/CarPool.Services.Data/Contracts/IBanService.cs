using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services.Data.Contracts
{
    public interface IBanService
    {
        Task RemoveBanAsync();
    }
}
