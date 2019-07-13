using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;

namespace Tripplanner.Business.ViewModels
{
    public class NewTripViewModel : ViewModelBase
    {
        private ITripRepository tripRepository;

        public NewTripViewModel(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
            CreateNewCommand = new MvxAsyncCommand(async () => await CreateNewTrip());
        }

        public MvxAsyncCommand CreateNewCommand { get; }
        public Action OnNavigateToTripDetails { get; set; }

        private string destination;
        public string Destination
        {
            get => destination;
            set
            {
                destination = value;
                RaisePropertyChanged(() => Destination);
            }
        }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        private async Task CreateNewTrip()
        {
            var trip = new Trip
            {
                TripId = Guid.NewGuid(),
                Destination = Destination,
                DateFrom = DateFrom,
                DateTo = DateTo
            };

            tripRepository.Add(trip);

            OnNavigateToTripDetails?.Invoke();
            await NavigationService.Navigate<TripDetailsViewModel, Trip>(trip);
            Messenger.Publish(new NewTripCreatedMessage(this, Destination));
        }
    }
}
