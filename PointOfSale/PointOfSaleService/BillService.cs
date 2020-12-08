using Dapper;
using Microsoft.Extensions.Configuration;
using PointOfSaleDomain;
using PointOfSaleService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class BillService : BaseService, IBillService
    {
        private readonly IConfiguration _configuration;
        public BillService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Money>> GetBillsAsync()
        {
            var cache = MemoryCache.Default;
            var cacheKey = "BillService|GetBillsAsync";

            var result = cache[cacheKey] as List<Money>;

            if (!Convert.ToBoolean(_configuration?.GetSection("UseCache")?.Value) || result == null)
            {
                result = (await Connection.QueryAsync<Money>("SELECT b.Value FROM Bill b")).ToList();

                if (result != null)
                {
                    cache.Set(cacheKey, result, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) });
                }
            }

            return result;
        }
    }
}
