using System;

namespace PointOfSaleInfra.Entities
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public decimal ValueToPay { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Change { get; set; }
        public string ChangeMessage { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
