using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class Accommodation : TripRelatedEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool UseTripDates { get; set; }
        public string Note { get; set; }
    }
}
