using Microsoft.VisualStudio.TestTools.UnitTesting;
using PointOfSaleDomain;
using PointOfSaleDomain.Messages;
using PointOfSaleServiceTests.Samples;
using System;
using System.Threading.Tasks;

namespace PointOfSaleService.Tests
{
    [TestClass()]
    public class ChangeServiceTests
    {
        private ChangeService ChangeServiceTest { get; set; }
        
        public ChangeServiceTests()
        {
            ChangeServiceTest = new ChangeService(new BillService(), new CoinService());
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSamples.DataInputsValid), typeof(DataSamples), DynamicDataSourceType.Method)]
        public async Task GetChangeTest(decimal valueToPay, decimal totalValue)
        {
            var result = await ChangeServiceTest.GetChangeAsync(new PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue });
            Assert.IsNotNull(result);
            Assert.IsTrue(result.TotalChange == Math.Round(valueToPay - totalValue, 2, MidpointRounding.ToZero));
        }

        [DataTestMethod]
        [DynamicData(nameof(DataSamples.DataInputsZeroOrNegative), typeof(DataSamples), DynamicDataSourceType.Method)]
        public async Task GetChangeZeroOrNegativeTest(decimal valueToPay, decimal totalValue)
        {
            try
            {
                var result = await ChangeServiceTest.GetChangeAsync(new PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message == Messages.HasNotValue);
            }
        }
        [DataTestMethod]
        [DynamicData(nameof(DataSamples.DataInputValueToPayIsSmaller), typeof(DataSamples), DynamicDataSourceType.Method)]
        public async Task GetChangeValueToPayIsSmallerTest(decimal valueToPay, decimal totalValue)
        {
            try
            {
                var result = await ChangeServiceTest.GetChangeAsync(new PointOfSale { ValueToPay = valueToPay, TotalValue = totalValue });
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message == Messages.ValueToPayIsSmaller);
            }
        }
    }
}