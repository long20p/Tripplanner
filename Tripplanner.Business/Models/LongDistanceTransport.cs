using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Models
{
    public class LongDistanceTransport : TripRelatedEntity
    {
        public LongDistanceTransportType TransportType { get; set; }
        public string TransportCompany { get; set; }
        public double Distance { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TicketNumber { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
