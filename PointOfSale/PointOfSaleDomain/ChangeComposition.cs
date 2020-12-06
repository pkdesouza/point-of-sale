using System.Collections.Generic;
using System.Linq;
using static PointOfSaleDomain.Messages.Messages;

namespace PointOfSaleDomain
{
    public class ChangeComposition
    {
        public ChangeComposition()
        {
            Coins = new List<Money>();
            MoneyBills = new List<Money>();
            TotalChange = 0;
        }
        public decimal TotalChange { get; private set; }
        public List<Money> Coins { get; set; }
        public List<Money> MoneyBills { get; set; }
        public string ResponseMessage { get => ToString(); }
        
        public void AddTotalChange(decimal value)
        {
            TotalChange += value;
        }

        public override string ToString() => string.Format(
                ResponseChange,
                TotalChange.ToString("c"),
                string.Join(", ", MoneyBills.GroupBy(x => x.Value).Select(s => new { Value = s.Key, Count = s.Count() }).Select(x => $"{x.Count}x {x.Value:c}").ToList()),
                string.Join(", ", Coins.GroupBy(x => x.Value).Select(s => new { Value = s.Key, Count = s.Count() }).Select(x => $"{x.Count}x {x.Value:c}").ToList())
        );

    }
}
