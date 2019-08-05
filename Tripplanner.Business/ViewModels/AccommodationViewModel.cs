using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class AccommodationViewModel : TripAwareViewModelBase
    {
        private IDialogService dialogService;
        private INotificationService notificationService;
        private IAccommodationRepository accommodationRepository;

        public AccommodationViewModel(IDialogService dialogService, IAccommodationRepository accommodationRepository,
            INotificationService notificationService)
        {
            this.dialogService = dialogService;
            this.accommodationRepository = accommodationRepository;
            this.notificationService = notificationService;
            CreateNewAccomItemCommand = GetAsyncCommand(async () => await CreateNewAccomItem());
        }

        public ICommand CreateNewAccomItemCommand { get; }

        private async Task CreateNewAccomItem()
        {
            await NavigationService.Navigate<NewAccommodationViewModel, Action<Accommodation>>(a =>
            {
                accommodationRepository.Add(a);
                notificationService.ShowInfo($"Entry for {a.Address} created");
            });
        }
    }
}
