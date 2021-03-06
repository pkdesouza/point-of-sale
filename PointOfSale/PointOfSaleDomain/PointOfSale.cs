﻿namespace PointOfSaleDomain
{
    public class PointOfSale
    {
        /// <summary>
        /// valor total a ser pago
        /// </summary>
        public decimal TotalValue { get; set; }
        /// <summary>
        /// valor efetivamente pago pelo cliente
        /// </summary>
        public decimal ValueToPay { get; set; }
    }
}
