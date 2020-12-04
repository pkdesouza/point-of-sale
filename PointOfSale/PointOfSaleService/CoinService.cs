using PointOfSaleDomain;
using PointOfSaleService.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class CoinService : ICoinService
    {
        public async Task<List<Money>> GetCoins()
        {
            return await Task.FromResult(new List<Money> {
                new Money { Value = 50},
                new Money { Value = 10},
                new Money { Value = 5},
                new Money { Value = 1}
            });
        }
    }
}
