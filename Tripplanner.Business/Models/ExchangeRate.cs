using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class ExchangeRate : TripRelatedEntity
    {
        public string SourceCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal AmountInSource { get; set; }
        public string LastRateDataSource { get; set; }
    }
}
