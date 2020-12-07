using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSaleInfra.Entities;
using PointOfSaleService;
using PointOfSaleServiceTests.Samples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSaleServiceTests
{
    [TestClass()]
    public class TransactionsServiceTests
    {
        public TransactionService TransactionService { get; set; }
        public ChangeService ChangeService { get; set; }

        public TransactionsServiceTests()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            TransactionService = new TransactionService(configuration);
            ChangeService = new ChangeService(new BillService(configuration), new CoinService(configuration), new TransactionService(configuration));
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSamples.DataInputsValid), typeof(DataSamples), DynamicDataSourceType.Method)]
        public async Task RegisterTransactionTest(decimal valueToPay, decimal totalValue)
        {
            try
            {
                var change = await ChangeService.GetChangeAsync(new PointOfSaleDomain.PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue });
                var result = (await GetAllTransactionsAsync())?.LastOrDefault(x => x.ChangeMessage == change.ResponseMessage);
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        public async Task<List<Transactions>> GetAllTransactionsAsync() => await TransactionService.GetAllAsync();
        
        [DataTestMethod]
        [DataRow()]
        public async Task GetAllTransactionsTest()
        {
            try
            {
                await GetAllTransactionsAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
