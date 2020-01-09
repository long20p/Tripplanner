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

        public bool SameDay => transport.StartTime.Day == transport.EndTime.Day
            && transport.StartTime.Month == transport.EndTime.Month
            && transport.StartTime.Year == transport.EndTime.Year;

        public string Time => $"{transport.StartTime:HH:mm:ss} - {transport.EndTime:HH:mm:ss}";

        public string Date => SameDay ? transport.StartTime.ToString("yyyy.MM.dd") : $"{transport.StartTime:yyyy.MM.dd} - {transport.EndTime:yyyy.MM.dd}";
    }
}
