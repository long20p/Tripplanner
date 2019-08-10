using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels.Wrappers;

namespace Tripplanner.Business.ViewModels
{
    public class AccommodationListViewModel : TripAwareViewModelBase
    {
        private IDialogService dialogService;
        private INotificationService notificationService;
        private IAccommodationRepository accommodationRepository;

        public AccommodationListViewModel(IDialogService dialogService, IAccommodationRepository accommodationRepository,
            INotificationService notificationService)
        {
            this.dialogService = dialogService;
            this.accommodationRepository = accommodationRepository;
            this.notificationService = notificationService;
            CreateNewAccomItemCommand = GetAsyncCommand(async () => await CreateNewAccomItem());
            Messenger.Subscribe<AccommodationDeletedMessage>(msg => RemoveAccommodation(msg.Accommodation));
        }

        public ICommand CreateNewAccomItemCommand { get; }

        private ObservableCollection<AccommodationViewModel> accommodations;

        public ObservableCollection<AccommodationViewModel> Accommodations
        {
            get => accommodations;
            set
            {
                accommodations = value;
                RaisePropertyChanged(() => Accommodations);
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public override async Task Initialize()
        {
            await LoadAccommodationForTrip();
            await base.Initialize();
        }

        private async Task LoadAccommodationForTrip()
        {
            IsLoading = true;
            var all = await Task.Run(() =>
                accommodationRepository.Where(x => x.TripId == Trip.TripId)
                    .Select(x => new AccommodationViewModel(x, Trip, accommodationRepository)).ToList());
            Accommodations = new ObservableCollection<AccommodationViewModel>(all);
            IsLoading = false;
        }

        private async Task CreateNewAccomItem()
        {
            await NavigationService.Navigate<NewAccommodationViewModel, Action<Accommodation>>(a =>
            {
                a.TripId = Trip.TripId;
                accommodationRepository.Add(a);
                Accommodations.Add(new AccommodationViewModel(a, Trip, accommodationRepository));
                RaisePropertyChanged(() => Accommodations);
                //notificationService.ShowInfo($"Entry for {a.Address} created");
            });
        }

        private void RemoveAccommodation(AccommodationViewModel accommodation)
        {
            Accommodations.Remove(accommodation);
            RaisePropertyChanged(() => Accommodations);
        }
    }
}
