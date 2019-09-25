using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class AccommodationViewModel : ViewModelBase
    {
        private Accommodation accommodation;
        private Trip trip;
        private readonly IAccommodationRepository accommodationRepository;
        private INotificationService notificationService;

        public AccommodationViewModel(Accommodation accommodation, Trip trip,
            IAccommodationRepository accommodationRepository, INotificationService notificationService)
        {
            this.accommodation = accommodation;
            this.trip = trip;
            this.accommodationRepository = accommodationRepository;
            this.notificationService = notificationService;
            DeleteAccommodationCommand = GetCommand(DeleteAccommodation);
            SelectEntryCommand = GetCommand(SelectEntry);
            UpdateEntryCommand = GetAsyncCommand(async () => await UpdateEntry());
        }

        public ICommand DeleteAccommodationCommand { get; }
        public ICommand SelectEntryCommand { get; }
        public ICommand UpdateEntryCommand { get; }

        public Accommodation Accommodation => accommodation;

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public string Dates => accommodation.UseTripDates
            ? $"{trip.DateFrom.ToString(Constants.DateFormat)} - {trip.DateTo.ToString(Constants.DateFormat)}"
            : $"{accommodation.From.Value.ToString(Constants.DateFormat)} - {accommodation.To.Value.ToString(Constants.DateFormat)}";

        private void DeleteAccommodation()
        {
            accommodationRepository.Delete(accommodation);
            PublishEvent(new AccommodationDeletedMessage(this));
        }

        private void SelectEntry()
        {
            IsSelected = !IsSelected;
        }

        private async Task UpdateEntry()
        {
            var editArgument = new AccommodationEditParam
            {
                CurrentAccommodation = accommodation,
                EditAction = a =>
                {
                    accommodationRepository.Update(a);
                    RaisePropertyChanged(() => Accommodation);
                    RaisePropertyChanged(() => Dates);
                    notificationService.ShowInfo($"Entry for {a.Name} updated");
                }
            };
            await NavigationService.Navigate<AccommodationEditViewModel, AccommodationEditParam>(editArgument);
        }
    }
}
