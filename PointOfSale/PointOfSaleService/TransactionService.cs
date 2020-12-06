using Dapper;
using PointOfSaleDomain;
using PointOfSaleInfra;
using PointOfSaleInfra.Entities;
using PointOfSaleService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class TransactionService : BaseService, ITransactionService
    {
        public PointOfSaleContext Context { get; set; }
        public TransactionService(PointOfSaleContext context)
        {
            Context = context;
        }

        public async Task Register(PointOfSale pointOfSale, ChangeComposition composition)
        {
            await Context.Transaction.AddAsync(new Transaction
            {
                ValueToPay = pointOfSale.ValueToPay,
                TotalValue = pointOfSale.TotalValue,
                Change = composition.TotalChange,
                ChangeMessage = composition.ResponseMessage,
                Date = DateTime.Now
            });
        }

        public async Task<List<Transaction>> GetAll(PointOfSale pointOfSale, ChangeComposition changeComposition)
        {
            using var con = Connection;
            return (await con.QueryAsync<Transaction>(@"SELECT  t.ValueToPay 
                                                                t.TotalValue,
                                                                t.Change,
                                                                t.ChangeMessage,
                                                                t.Date 
                                                       FROM Transaction t")).ToList();
        }
    }
}
