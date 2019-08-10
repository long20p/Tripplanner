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
    public class AllTripsViewModel : ViewModelBase
    {
        private ITripRepository tripRepository;
        private IDispatcherService dispatcherService;

        public AllTripsViewModel(ITripRepository tripRepository, IDispatcherService dispatcherService)
        {
            this.tripRepository = tripRepository;
            this.dispatcherService = dispatcherService;
            SelectTripCommand = GetAsyncCommand<TripViewModel>(async trip => await SelectTrip(trip));
            Messenger.Subscribe<NewTripCreatedMessage>( msg => AddTrip(msg.Trip));
            Messenger.Subscribe<TripDeletedMessage>( msg => RemoveTrip(msg.Trip));
        }

        public ICommand SelectTripCommand { get; }

        private ObservableCollection<TripViewModel> trips;
        public ObservableCollection<TripViewModel> Trips
        {
            get => trips;
            set
            {
                trips = value;
                RaisePropertyChanged(() => Trips);
            }
        }

        private bool isLoadingTrips;
        public bool IsLoadingTrips
        {
            get => isLoadingTrips;
            set
            {
                isLoadingTrips = value;
                RaisePropertyChanged(() => IsLoadingTrips);
            }
        }

        public override async Task Initialize()
        {
            await RefreshTripList();
            await base.Initialize();
        }

        private async Task RefreshTripList()
        {
            IsLoadingTrips = true;
            var allTrips = await Task.Run(() => tripRepository.GetAll().Select(x => new TripViewModel(x, tripRepository)).ToList());
            Trips = new ObservableCollection<TripViewModel>(allTrips);
            IsLoadingTrips = false;
        }

        private async Task SelectTrip(TripViewModel trip)
        {
            await NavigationService.Navigate<TripDetailsViewModel, Trip>(trip.Trip);
        }

        private void RemoveTrip(TripViewModel trip)
        {
            Trips.Remove(trip);
            RaisePropertyChanged(() => Trips);
        }

        private void AddTrip(Trip trip)
        {
            Trips.Add(new TripViewModel(trip, tripRepository));
            RaisePropertyChanged(() => Trips);
        }
    }
}
