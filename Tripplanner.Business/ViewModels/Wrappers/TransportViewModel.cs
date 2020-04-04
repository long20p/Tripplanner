using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class TransportViewModel : ViewModelBase
    {
        private LongDistanceTransport transport;
        private ITransportRepository transportRepository;
        private INotificationService notificationService;

        public TransportViewModel(LongDistanceTransport transport, ITransportRepository transportRepository, INotificationService notificationService)
        {
            this.transport = transport;
            this.transportRepository = transportRepository;
            this.notificationService = notificationService;
            DeleteEntryCommand = GetCommand(DeleteEntry);
            UpdateEntryCommand = GetAsyncCommand(UpdateEntry);
        }

        public ICommand UpdateEntryCommand { get; }
        public ICommand DeleteEntryCommand { get; }

        public LongDistanceTransport Transport => transport;

        public string Title => $"{transport.TransportCompany} {transport.TransportType}";

        public string Locations => $"{transport.StartLocation} - {transport.EndLocation}";

        public bool SameDay => transport.DepartureDate.Day == transport.ArrivalDate.Day
            && transport.DepartureDate.Month == transport.ArrivalDate.Month
            && transport.DepartureDate.Year == transport.ArrivalDate.Year;

        public string Time => $"{transport.DepartureTime} - {transport.ArrivalTime}";

        public string Date => SameDay ? transport.DepartureDate.ToString("yyyy.MM.dd") : $"{transport.DepartureDate:yyyy.MM.dd} - {transport.ArrivalDate:yyyy.MM.dd}";

        private void DeleteEntry()
        {
            transportRepository.Delete(transport);
            PublishEvent(new TransportDeletedMessage(this));
        }

        private async Task UpdateEntry()
        {
            var editArgument = new TransportEditParam
            {
                CurrentTransport = transport,
                EditAction = t =>
                {
                    transportRepository.Update(t);
                    RaisePropertyChanged(() => Transport);
                    RaisePropertyChanged(() => Title);
                    RaisePropertyChanged(() => Locations);
                    RaisePropertyChanged(() => Time);
                    RaisePropertyChanged(() => Date);
                    notificationService.ShowInfo($"Transport entry updated");
                }
            };
            await NavigationService.Navigate<TransportEditViewModel, TransportEditParam>(editArgument);
        }
    }
}
