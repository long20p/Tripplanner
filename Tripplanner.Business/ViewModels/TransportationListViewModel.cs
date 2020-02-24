using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.ViewModels
{
    public class TransportationListViewModel : TripAwareViewModelBase
    {
        private ITransportRepository transportRepository;
        private INotificationService notificationService;

        public TransportationListViewModel(ITransportRepository transportRepository, INotificationService notificationService)
        {
            this.transportRepository = transportRepository;
            this.notificationService = notificationService;
            CreateNewTransportItemCommand = GetAsyncCommand(CreateNewTransportItem);
            SubscribeToEvent<TransportDeletedMessage>(msg => RemoveTransportItem(msg.Transport));
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
            .Select(x => new TransportViewModel(x, transportRepository, notificationService)).ToList());

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
                    Transports.Add(new TransportViewModel(transport, transportRepository, notificationService));
                    RaisePropertyChanged(() => Transports);
                }
            };

            await NavigationService.Navigate<TransportEditViewModel, TransportEditParam>(editParam);
        }

        private void RemoveTransportItem(TransportViewModel transport)
        {
            Transports.Remove(transport);
            RaisePropertyChanged(() => Transports);
        }
    }
}
