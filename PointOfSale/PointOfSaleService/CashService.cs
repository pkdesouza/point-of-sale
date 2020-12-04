using PointOfSaleDomain;
using PointOfSaleService.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class CashService : ICashService
    {
        public async Task<List<Money>> GetCashesAsync()
        {
            return await Task.FromResult(
                new List<Money> {
                    new Money { Value = 100},
                    new Money { Value = 50},
                    new Money { Value = 20},
                    new Money { Value = 10}
                });
        }
    }
}
