using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.ViewModels
{
    public class TripDetailsViewModel : ViewModelBase<Trip>
    {
        private IMvxNavigationService navigationService;
        private Trip trip;

        public TripDetailsViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            GoToTransportCommand = new MvxAsyncCommand( async () => await GoToTransportation());
        }

        public MvxAsyncCommand GoToTransportCommand { get; }

        public override void Prepare(Trip parameter)
        {
            trip = parameter;
        }

        private async Task GoToTransportation()
        {
            await navigationService.Navigate<TransportationViewModel>();
        }
    }
}
