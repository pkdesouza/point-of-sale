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
        private IBillService CashService { get; set; }
        private ICoinService CoinService { get; set; }
        private ITransactionService TransactionService { get; set; }
        private List<Money> Coins { get; set; }
        private List<Money> MoneyBills { get; set; }

        public ChangeService(IBillService cashService, ICoinService coinService, ITransactionService transactionService)
        {
            CashService = cashService;
            CoinService = coinService;
            TransactionService = transactionService;
        }

        public async Task<ChangeComposition> GetChangeAsync(PointOfSale pointOfSale)
        {
            pointOfSale.Validate();
            MoneyBills = await CashService.GetBillsAsync();
            Coins = await CoinService.GetCoinsAsync();
            var result = CalculeChange(pointOfSale.ValueToPay - pointOfSale.TotalValue, new ChangeComposition());
            await TransactionService.Register(pointOfSale, result);
            return result;
        }

        private ChangeComposition CalculeChange(decimal change, ChangeComposition changeComposition)
        {
            SetChange(change, changeComposition, MoneyBills, changeComposition.MoneyBills);
            SetChange(change, changeComposition, Coins, changeComposition.Coins);
            return changeComposition;
        }

        private void SetChange(decimal change, ChangeComposition changeComposition, List<Money> moneyOnAccount, List<Money> moneyToReceive)
        {
            for (int i = 0; i < moneyOnAccount.Count && changeComposition.TotalChange < change; i++)
            {
                var amount = moneyOnAccount.ElementAt(i);
                var value = amount.Value;

                if (value <= change - changeComposition.TotalChange)
                {
                    moneyToReceive.Add(amount);
                    changeComposition.AddTotalChange(value);
                    i--;
                }
            }
        }
    }
}
