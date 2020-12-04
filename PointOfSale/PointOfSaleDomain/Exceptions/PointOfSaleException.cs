using System;

namespace PointOfSaleDomain.Exceptions
{
    public class PointOfSaleException: Exception
    {
        public PointOfSaleException(string message) : base(message) { }
    }
}
