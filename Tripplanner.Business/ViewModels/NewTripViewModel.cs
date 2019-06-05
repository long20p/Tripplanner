using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;

namespace Tripplanner.Business.ViewModels
{
    public class NewTripViewModel : ViewModelBase
    {
        public NewTripViewModel()
        {
            CreateNewCommand = new MvxAsyncCommand(async () => await CreateNewTrip());
        }

        public MvxAsyncCommand CreateNewCommand { get; }
        public Action OnNavigateToTripDetails { get; set; }

        private async Task CreateNewTrip()
        {
            OnNavigateToTripDetails?.Invoke();
            await NavigationService.Navigate<TripDetailsViewModel>();
        }
    }
}
