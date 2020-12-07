using Bogus;
using System.Collections.Generic;

namespace PointOfSaleServiceTests.Samples
{
    public class DataSamples
    {
        public readonly static Faker DataMock = new Faker(locale: "pt_BR");
        public static IEnumerable<object[]> DataInputsValid()
        {
            yield return new object[] { 30.11M, 27.30M };
            yield return new object[] { 92.4912M, 23.340M };
            yield return new object[] { 13.1M, 3M };
            yield return new object[] { 9.57M, 9.57M };
            yield return new object[] { DataMock.Random.Decimal(10, 100), DataMock.Random.Decimal(1, 10) };
            yield return new object[] { DataMock.Random.Decimal(20, 200), DataMock.Random.Decimal(10, 20) };
            yield return new object[] { DataMock.Random.Decimal(30, 300), DataMock.Random.Decimal(10, 30) };
            yield return new object[] { DataMock.Random.Decimal(1000, 10000), DataMock.Random.Decimal(100, 900) };
            yield return new object[] { DataMock.Random.Decimal(1, 10), DataMock.Random.Decimal(0, 1) };
        }
        public static IEnumerable<object[]> DataInputsZeroOrNegative()
        {
            yield return new object[] { 0M, 0M };
            yield return new object[] { 0M, 1M };
            yield return new object[] { 1M, 0M };
            yield return new object[] { -1M, 0M };
            yield return new object[] { 1M, -20M };
        }
        public static IEnumerable<object[]> DataInputValueToPayIsSmaller()
        {
            yield return new object[] { 10M, 50M };
            yield return new object[] { 1M, 2M };
            yield return new object[] { 0.022M, 0.025M };
            yield return new object[] { DataMock.Random.Decimal(10, 20), DataMock.Random.Decimal(20, 1000) };
        }
    }
}
