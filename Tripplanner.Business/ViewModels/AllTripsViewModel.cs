using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.ViewModels
{
    public class AllTripsViewModel : ViewModelBase
    {
        private ITripRepository tripRepository;

        public AllTripsViewModel(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
            Messenger.Subscribe<NewTripCreatedMessage>(_ => RefreshTripList());
            Messenger.Subscribe<TripDeletedMessage>(_ => RefreshTripList());
        }

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

        public override Task Initialize()
        {
            RefreshTripList();
            return base.Initialize();
        }

        private void RefreshTripList()
        {
            Trips = new ObservableCollection<TripViewModel>(tripRepository.GetAll()
                .Select(x => new TripViewModel(x, tripRepository)));
        }
    }
}
