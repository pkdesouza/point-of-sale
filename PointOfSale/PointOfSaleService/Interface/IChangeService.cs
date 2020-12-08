using PointOfSaleDomain;
using System.Threading.Tasks;
namespace PointOfSaleService.Interface
{
    public interface IChangeService
    {
        Task<ChangeComposition> GetChangeAsync(PointOfSale pointOfSale);
    }
}
