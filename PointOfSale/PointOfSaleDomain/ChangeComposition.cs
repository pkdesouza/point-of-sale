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

        public List<Money> Coins { get; set; }
        public List<Money> MoneyBills { get; set; }
        public decimal TotalChange { get; set; }
        public string ResponseMessage { get; set; }

        public override string ToString() => string.Format(
                ResponseChange,
                TotalChange.ToString("c"),
                string.Join(", ", MoneyBills.GroupBy(x => x.Value).Select(s => new { Value = s.Key, Count = s.Count() }).Select(x => $"{x.Count}x {x.Value:c}").ToList()),
                string.Join(", ", Coins.GroupBy(x => x.Value).Select(s => new { Value = s.Key, Count = s.Count() }).Select(x => $"{x.Count}x {x.Value / 100:c}").ToList())
        );

    }
}
