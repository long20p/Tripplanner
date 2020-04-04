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
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string TicketNumber { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
