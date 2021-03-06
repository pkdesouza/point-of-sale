﻿using PointOfSaleDomain;
using PointOfSaleService.Interface;
using PointOfSaleDomain.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace PointOfSaleService
{
    public class ChangeService : IChangeService
    {
        private IBillService BillService { get; set; }
        private ICoinService CoinService { get; set; }
        private ITransactionService TransactionService { get; set; }
        private List<Money> Coins { get; set; }
        private List<Money> Bills { get; set; }

        public ChangeService(IBillService billService, ICoinService coinService, ITransactionService transactionService)
        {
            BillService = billService;
            CoinService = coinService;
            TransactionService = transactionService;
        }

        public async Task<ChangeComposition> GetChangeAsync(PointOfSale pointOfSale)
        {
            pointOfSale.Validate();
            Bills = await BillService.GetBillsAsync();
            Coins = await CoinService.GetCoinsAsync();
            var result = CalculeChange(Math.Round(pointOfSale.ValueToPay - pointOfSale.TotalValue, 2, MidpointRounding.ToZero), new ChangeComposition());
            await TransactionService.RegisterAsync(pointOfSale, result);
            return result;
        }

        private ChangeComposition CalculeChange(decimal change, ChangeComposition changeComposition)
        {
            SetChange(change, changeComposition, Bills, changeComposition.Bills);
            SetChange(change, changeComposition, Coins, changeComposition.Coins);
            return changeComposition;
        }

        private void SetChange(decimal change, ChangeComposition changeComposition, List<Money> moneyOnAccount, List<Money> moneyToReceive)
        {
            for (int i = 0; i < moneyOnAccount.Count && changeComposition.TotalChange < change; i++)
            {
                var amount = moneyOnAccount.ElementAt(i);
                var value = amount.Value;

                if (value <= Math.Round(change - changeComposition.TotalChange, 2, MidpointRounding.ToZero))
                {
                    moneyToReceive.Add(amount);
                    changeComposition.AddTotalChange(value);
                    i--;
                }
            }
        }
    }
}
