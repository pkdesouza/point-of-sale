using System;

namespace PointOfSale.Exceptions
{
    public class PointOfSaleException: Exception
    {
        public PointOfSaleException(string message) : base(message) { }
    }
}
