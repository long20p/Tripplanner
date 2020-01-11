using System;
using System.Collections.Generic;
using System.Text;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class TransportViewModel
    {
        private LongDistanceTransport transport;
        private ITransportRepository transportRepository;

        public TransportViewModel(LongDistanceTransport transport, ITransportRepository transportRepository)
        {
            this.transport = transport;
            this.transportRepository = transportRepository;
        }

        public LongDistanceTransport Transport => transport;

        public string Title => $"{transport.TransportCompany} {transport.TransportType}";

        public string Locations => $"{transport.StartLocation} - {transport.EndLocation}";

        public bool SameDay => transport.DepartureDate.Day == transport.ArrivalDate.Day
            && transport.DepartureDate.Month == transport.ArrivalDate.Month
            && transport.DepartureDate.Year == transport.ArrivalDate.Year;

        public string Time => $"{transport.DepartureDate:HH:mm:ss} - {transport.ArrivalDate:HH:mm:ss}";

        public string Date => SameDay ? transport.DepartureDate.ToString("yyyy.MM.dd") : $"{transport.DepartureDate:yyyy.MM.dd} - {transport.ArrivalDate:yyyy.MM.dd}";
    }
}
