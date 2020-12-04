using static PointOfSaleDomain.Messages.Messages;
using System;
using PointOfSaleDomain.Exceptions;

namespace PointOfSaleDomain.Extensions
{
    public static class PointOfSaleDomainExtensions
    {
        public static Func<decimal, decimal, bool> IsBigger = (x, y) => x > y;
        public static Func<decimal, decimal, bool> HasValue = (x, y) => x > 0 && y > 0;
        public static void Validate(this PointOfSale pointOfSale)
        {
            if (!IsBigger(pointOfSale.ValueToPay, pointOfSale.TotalValue))
                throw new PointOfSaleException(ValueToPayIsSmaller);
            if(!HasValue(pointOfSale.ValueToPay, pointOfSale.TotalValue))
                throw new PointOfSaleException(HasNotValue);
        }
    }
}
