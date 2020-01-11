using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.ViewModels
{
    public class TransportationListViewModel : TripAwareViewModelBase
    {
        private ITransportRepository transportRepository;

        public TransportationListViewModel(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
            CreateNewTransportItemCommand = GetAsyncCommand(CreateNewTransportItem);
            IndeterminateLoading = true;
        }

        public ICommand CreateNewTransportItemCommand { get; }

        private ObservableCollection<TransportViewModel> transports;
        public ObservableCollection<TransportViewModel> Transports
        {
            get => transports;
            set
            {
                transports = value;
                RaisePropertyChanged(() => Transports);
            }
        }

        public bool IndeterminateLoading { get; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public override async Task Initialize()
        {
            await LoadTransportsForTrip();
            await base.Initialize();
        }

        private async Task LoadTransportsForTrip()
        {
            IsLoading = true;
            var all = await Task.Run(() => transportRepository.Where(x => x.TripId == Trip.UniqueId)
            .Select(x => new TransportViewModel(x, transportRepository)).ToList());

            //var all = new List<TransportViewModel>
            //{
            //    new TransportViewModel(
            //        new LongDistanceTransport
            //        { 
            //            TransportType = LongDistanceTransportType.Flight, 
            //            TransportCompany = "Volotea", 
            //            StartLocation = "Naples", 
            //            EndLocation = "Palermo",
            //            DepartureDate = new DateTime(2020, 1, 12, 12, 35, 00),
            //            ArrivalDate = new DateTime(2020, 1, 12, 14, 05, 00)
            //        },
            //        transportRepository),
            //    new TransportViewModel(
            //        new LongDistanceTransport
            //        {
            //            TransportType = LongDistanceTransportType.Ship,
            //            TransportCompany = "Garibaldi",
            //            StartLocation = "Catania",
            //            EndLocation = "Salerno",
            //            DepartureDate = new DateTime(2020, 1, 15, 23, 30, 00),
            //            ArrivalDate = new DateTime(2020, 1, 16, 9, 00, 00)
            //        },
            //        transportRepository)
            //};

            Transports = new ObservableCollection<TransportViewModel>(all);
            IsLoading = false;
        }

        private async Task CreateNewTransportItem()
        {
            var editParam = new TransportEditParam
            {
                EditAction = transport =>
                {
                    transport.TripId = Trip.UniqueId;
                    transportRepository.Add(transport);
                    Transports.Add(new TransportViewModel(transport, transportRepository));
                    RaisePropertyChanged(() => Transports);
                }
            };

            await NavigationService.Navigate<TransportEditViewModel, TransportEditParam>(editParam);
        }
    }
}
