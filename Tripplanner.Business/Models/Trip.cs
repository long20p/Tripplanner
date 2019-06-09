using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class Trip : BaseEntity
    {
        public Guid TripId { get; set; }
        public string Destination { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
