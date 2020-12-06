using PointOfSaleDomain;
using PointOfSaleInfra.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PointOfSaleService.Interface
{
    public interface ITransactionService
    {
        Task Register(PointOfSale pointOfSale, ChangeComposition changeComposition);

        Task<List<Transaction>> GetAll();
    }
}
