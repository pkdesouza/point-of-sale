using Dapper;
using PointOfSaleDomain;
using PointOfSaleService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PointOfSaleService
{
    public class CoinService : BaseService, ICoinService
    {
        private readonly IConfiguration _configuration;
        public CoinService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Money>> GetCoinsAsync()
        {
            var cache = MemoryCache.Default;
            var cacheKey = "CoinService|GetCoinsAsync";

            var result = cache[cacheKey] as List<Money>;

            if (!Convert.ToBoolean(_configuration?.GetSection("UseCache")?.Value) || result == null)
            {
                result = (await Connection.QueryAsync<Money>("SELECT c.Value / 100 as Value FROM Coin c")).ToList();

                if (result != null)
                {
                    cache.Set(cacheKey, result, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) });
                }
            }

            return result;
        }
    }
}
