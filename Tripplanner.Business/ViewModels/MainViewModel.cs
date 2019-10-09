using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public void NavigateToPage(string viewModelName)
        {
            switch (viewModelName)
            {
                case nameof(NewTripViewModel):
                    NavigationService.Navigate<NewTripViewModel>();
                    break;
                case nameof(AllTripsViewModel):
                    NavigationService.Navigate<AllTripsViewModel>();
                    break;
                case nameof(SettingsViewModel):
                    NavigationService.Navigate<SettingsViewModel>();
                    break;
                case nameof(BackupViewModel):
                    NavigationService.Navigate<BackupViewModel>();
                    break;
                case nameof(RestoreViewModel):
                    NavigationService.Navigate<RestoreViewModel>();
                    break;
                case nameof(AboutViewModel):
                    NavigationService.Navigate<AboutViewModel>();
                    break;
                default:
                    throw new Exception($"Unknown page: {viewModelName}");
            }
        }
    }
}
