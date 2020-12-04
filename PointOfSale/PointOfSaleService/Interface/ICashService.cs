using PointOfSaleDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleService.Interface
{
    public interface ICashService
    {
        Task<List<Money>> GetCashesAsync();
    }
}
