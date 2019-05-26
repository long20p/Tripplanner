using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Tripplanner.Business.ViewModels
{
    public class NewTripViewModel : ViewModelBase
    {
        private IMvxNavigationService navigationService;

        public NewTripViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            CreateNewCommand = new MvxAsyncCommand(async () => await CreateNewTrip());
        }

        public MvxAsyncCommand CreateNewCommand { get; }
        public Action OnNavigateToTripDetails { get; set; }

        private async Task CreateNewTrip()
        {
            OnNavigateToTripDetails?.Invoke();
            await navigationService.Navigate<TripDetailsViewModel>();
            
        }
    }
}
