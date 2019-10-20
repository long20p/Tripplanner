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
            GoToExchangeRateCommand = GetAsyncCommand(async () => await GoToExchangeRate());
        }

        public ICommand GoToTransportCommand { get; }
        public ICommand GoToAccommodationCommand { get; }
        public ICommand GoToActivitiesCommand { get; }
        public ICommand GoToCityTransportCommand { get; }
        public ICommand GoToExchangeRateCommand { get; }

        public string TripDate => $"{base.Trip.DateFrom:dd/MM/yy} - {Trip.DateTo:dd/MM/yy}";
        public string Destination => Trip.Destination;

        private async Task GoToTransportation()
        {
            await NavigationService.Navigate<TransportationViewModel, Trip>(Trip);
        }

        private async Task GoToAccomodation()
        {
            await NavigationService.Navigate<AccommodationListViewModel, Trip>(Trip);
        }

        private async Task GoToExchangeRate()
        {
            await NavigationService.Navigate<CurrencyExchangeViewModel, Trip>(Trip);
        }
    }
}
