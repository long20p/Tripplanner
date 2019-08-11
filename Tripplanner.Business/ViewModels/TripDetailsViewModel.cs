using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.ViewModels
{
    public class TripDetailsViewModel : TripAwareViewModelBase
    {
        public TripDetailsViewModel()
        {
            GoToTransportCommand = GetAsyncCommand( async () => await GoToTransportation());
            GoToAccommodationCommand = GetAsyncCommand(async () => await GoToAccomodation());
        }

        public ICommand GoToTransportCommand { get; }
        public ICommand GoToAccommodationCommand { get; }
        public ICommand GoToActivitiesCommand { get; }
        public ICommand GoToCityTransportCommand { get; }

        public Trip Trip => trip;
        public string TripDate => $"{trip.DateFrom:dd/MM/yy} - {trip.DateTo:dd/MM/yy}";

        private async Task GoToTransportation()
        {
            await NavigationService.Navigate<TransportationViewModel, Trip>(trip);
        }

        private async Task GoToAccomodation()
        {
            await NavigationService.Navigate<AccommodationViewModel, Trip>(trip);
        }
    }
}
