using System;

namespace PointOfSaleInfra.Entities
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public decimal ValueToPay { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Change { get; set; }
        public string ChangeMessage { get; set; }
        public DateTime Date { get; set; }
    }
}
