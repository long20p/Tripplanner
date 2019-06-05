using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Tripplanner.Business.ViewModels
{
    public class TripDetailsViewModel : ViewModelBase
    {
        private IMvxNavigationService navigationService;

        public TripDetailsViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            GoToTransportCommand = new MvxAsyncCommand( async () => await GoToTransportation());
        }

        public MvxAsyncCommand GoToTransportCommand { get; }

        private async Task GoToTransportation()
        {
            await navigationService.Navigate<TransportationViewModel>();
        }
    }
}
