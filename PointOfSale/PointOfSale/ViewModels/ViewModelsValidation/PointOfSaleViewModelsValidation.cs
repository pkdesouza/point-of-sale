using PointOfSale.Exceptions;
using System;

namespace PointOfSale.ViewModels.ViewModelsValidation
{
    public static class PointOfSaleViewModelsValidation
    {
        public static Func<decimal, decimal, bool> ValueToPayIsBiggerOrEqual = (ValueToPay, TotalValue) => ValueToPay >= TotalValue;
        public static bool IsValid(this PointOfSaleViewModel pointOfSale)
        {
            if (!ValueToPayIsBiggerOrEqual(pointOfSale.ValueToPay, pointOfSale.TotalValue))
                throw new PointOfSaleException(Messages.ValueToPayIsSmaller);
            return true;
        }
    }
}
