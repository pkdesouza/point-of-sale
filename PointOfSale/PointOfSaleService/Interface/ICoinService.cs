using PointOfSaleDomain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleService.Interface
{
    public interface ICoinService
    {
        Task<List<Money>> GetCoins();
    }
}
