using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;

namespace Tripplanner.Business.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateToPage(string viewModelName)
        {
            switch (viewModelName)
            {
                case nameof(NewTripViewModel):
                    _navigationService.Navigate<NewTripViewModel>();
                    break;
                case nameof(AllTripsViewModel):
                    _navigationService.Navigate<AllTripsViewModel>();
                    break;
                case nameof(SettingsViewModel):
                    _navigationService.Navigate<SettingsViewModel>();
                    break;
                case nameof(BackupViewModel):
                    _navigationService.Navigate<BackupViewModel>();
                    break;
                case nameof(AboutViewModel):
                    _navigationService.Navigate<AboutViewModel>();
                    break;
                default:
                    throw new Exception($"Unknown page: {viewModelName}");
            }
        }
    }
}
