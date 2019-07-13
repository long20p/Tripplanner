using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
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
            DeleteTripCommand = new MvxAsyncCommand(async () => await DeleteTrip());
        }

        public MvxAsyncCommand DeleteTripCommand { get; }

        public string Destination => trip.Destination;
        public string TripId => trip.TripId.ToString();
        public string Dates => $"{trip.DateFrom:dd.MM.yyyy} - {trip.DateTo:dd.MM.yyyy}";

        private async Task DeleteTrip()
        {
            tripRepository.Delete(trip);
            Messenger.Publish(new TripDeletedMessage(this));
        }
    }
}
