using Dapper;
using PointOfSaleDomain;
using PointOfSaleService.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class CoinService : BaseService, ICoinService
    {
        public async Task<List<Money>> GetCoinsAsync()
        {
            using var con = Connection;
            return (await con.QueryAsync<Money>("SELECT c.Value / 100 as Value FROM Coin c")).ToList();
        }
    }
}
