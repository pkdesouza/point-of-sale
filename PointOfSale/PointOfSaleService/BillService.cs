using Dapper;
using PointOfSaleDomain;
using PointOfSaleService.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class BillService : BaseService, IBillService
    {
        public async Task<List<Money>> GetBillsAsync()
        {
            using var con = Connection;
            return (await con.QueryAsync<Money>("SELECT b.Value FROM Bill b")).ToList();
        }
    }
}
