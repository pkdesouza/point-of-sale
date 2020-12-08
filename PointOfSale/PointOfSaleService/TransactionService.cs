using Dapper;
using Microsoft.Extensions.Configuration;
using PointOfSaleDomain;
using PointOfSaleInfra;
using PointOfSaleInfra.Entities;
using PointOfSaleService.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleService
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly IConfiguration _configuration;
        public TransactionService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task RegisterAsync(PointOfSale pointOfSale, ChangeComposition composition)
        {
            using var context = new PointOfSaleContext(_configuration);
            await context.Transactions.AddAsync(new Transactions
            {
                ValueToPay = pointOfSale.ValueToPay,
                TotalValue = pointOfSale.TotalValue,
                Change = composition.TotalChange,
                ChangeMessage = composition.ResponseMessage
            });
            await context.SaveChangesAsync();
        }

        public async Task<List<Transactions>> GetAllAsync()
        {
            using var con = Connection;
            return (await con.QueryAsync<Transactions>(@"SELECT t.Id, 
                                                                t.ValueToPay,
                                                                t.TotalValue,
                                                                t.Change,
                                                                t.ChangeMessage,
                                                                t.RegistrationDate 
                                                       FROM Transactions t")).ToList();
        }
    }
}
