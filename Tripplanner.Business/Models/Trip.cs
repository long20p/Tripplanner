using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class Trip : BaseEntity, ICopyable<Trip>
    {
        public string Destination { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public void CopyFrom(Trip other)
        {
            Destination = other.Destination;
            DateFrom = other.DateFrom;
            DateTo = other.DateTo;
        }
    }
}
