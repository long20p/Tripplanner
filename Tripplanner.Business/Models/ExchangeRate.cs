using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tripplanner.Business.Models
{
    public class ExchangeRate : TripRelatedEntity
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal AmountInSource { get; set; }
        public decimal LastRate { get; set; }
        public DateTime LastRateValidAt { get; set; }

        [Ignore]
        public string AmountInTarget => $"{(AmountInSource * LastRate):##.####}";
    }
}
