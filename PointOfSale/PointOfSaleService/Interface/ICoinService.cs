using PointOfSaleDomain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOfSaleService.Interface
{
    public interface ICoinService
    {
        Task<List<Money>> GetCoinsAsync();
    }
}
