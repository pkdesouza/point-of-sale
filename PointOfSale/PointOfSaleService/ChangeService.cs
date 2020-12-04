using PointOfSaleDomain;
using PointOfSaleService.Interface;
using PointOfSaleDomain.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class ChangeService : IChangeService
    {
        public ICashService CashService { get; set; }
        public ICoinService CoinService { get; set; }
        private List<Money> Coins { get; set; }
        private List<Money> Cashes { get; set; }

        public ChangeService(ICashService cashService, ICoinService coinService)
        {
            CashService = cashService;
            CoinService = coinService;
        }
        public async Task<ChangeComposition> GetChange(PointOfSale pointOfSale)
        {
            pointOfSale.Validate();
            Cashes = await CashService.GetCashesAsync();
            Coins = await CoinService.GetCoins();

            var result = CalculeChange(pointOfSale.ValueToPay - pointOfSale.TotalValue, new ChangeComposition());
            result.ResponseMessage = result.ToString();
            return result;
        }

        private ChangeComposition CalculeChange(decimal change, ChangeComposition charge)
        {
            for (int i = 0; i < Cashes.Count && charge.TotalChange < change; i++)
            {
                var bill = Cashes.ElementAt(i);
                var billValue = bill.Value;

                if (billValue <= change - charge.TotalChange)
                {
                    charge.MoneyBills.Add(bill);
                    charge.TotalChange += billValue;
                    i--;
                }
            }

            for (int i = 0; i < Coins.Count && charge.TotalChange < change; i++)
            {
                var coin = Coins.ElementAt(i);
                var coinValue = coin.Value / 100;

                if (coinValue <= change - charge.TotalChange)
                {
                    charge.Coins.Add(coin);
                    charge.TotalChange += coinValue;
                    i--;
                }
            }

            return charge;
        }


    }
}
