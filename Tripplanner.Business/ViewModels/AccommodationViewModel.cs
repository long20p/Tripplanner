using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class AccommodationViewModel : TripAwareViewModelBase
    {
        private IDialogService dialogService;

        public AccommodationViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
            CreateNewAccomItemCommand = GetAsyncCommand(async () => await CreateNewAccomItem());
        }

        public ICommand CreateNewAccomItemCommand { get; }

        private async Task CreateNewAccomItem()
        {
            //dialogService.ShowDialog(() => Messenger.Publish(new NewAccommodationCreatedMessage(this)), () => {});
            await NavigationService.Navigate<NewAccommodationViewModel>();
        }
    }
}
