using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class TripViewModel : ViewModelBase
    {
        private readonly Trip trip;
        private ITripRepository tripRepository;

        public TripViewModel(Trip trip, ITripRepository tripRepository)
        {
            this.trip = trip;
            this.tripRepository = tripRepository;
            DeleteTripCommand = GetCommand(DeleteTrip);
        }

        public ICommand DeleteTripCommand { get; }

        public Trip Trip => trip;
        public string Destination => trip.Destination;
        public string TripId => trip.UniqueId.ToString();
        public string Dates => $"{trip.DateFrom.ToString(Constants.DateFormat)} - {trip.DateTo.ToString(Constants.DateFormat)}";

        private void DeleteTrip()
        {
            tripRepository.Delete(trip);
            Messenger.Publish(new TripDeletedMessage(this));
        }
    }
}
