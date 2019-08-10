using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class NewTripViewModel : ViewModelBase
    {
        private ITripRepository tripRepository;

        public NewTripViewModel(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
            CreateNewCommand = GetAsyncCommand(async () => await CreateNewTrip());
            OpenFromCalendarCommand = GetAsyncCommand(async () => await OpenFromCalendar());
            OpenToCalendarCommand = GetAsyncCommand(async () => await OpenToCalendar());
        }

        public ICommand CreateNewCommand { get; }
        public ICommand OpenFromCalendarCommand { get; }
        public ICommand OpenToCalendarCommand { get; }
        public Action OnNavigateToTripDetails { get; set; }

        private string destination;
        public string Destination
        {
            get => destination;
            set
            {
                destination = value;
                RaisePropertyChanged(() => Destination);
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get => dateFrom;
            set
            {
                dateFrom = value;
                DateFromText = dateFrom.ToString("dd.MM.yyyy");
            }
        }

        private string dateFromText;
        public string DateFromText
        {
            get => dateFromText;
            set
            {
                dateFromText = value;
                RaisePropertyChanged(() => DateFromText);
            }
        }

        private DateTime dateTo;
        public DateTime DateTo
        {
            get => dateTo;
            set
            {
                dateTo = value;
                DateToText = dateTo.ToString("dd.MM.yyyy");
            }
        }

        private string dateToText;
        public string DateToText
        {
            get => dateToText;
            set
            {
                dateToText = value;
                RaisePropertyChanged(() => DateToText);
            }
        }

        private async Task OpenFromCalendar()
        {
            await NavigationService.Navigate<DateSelectorViewModel, Action<DateTime>>(from => DateFrom = from);
        }

        private async Task OpenToCalendar()
        {
            await NavigationService.Navigate<DateSelectorViewModel, Action<DateTime>>(to => DateTo = to);
        }

        private async Task CreateNewTrip()
        {
            var trip = new Trip
            {
                TripId = Guid.NewGuid(),
                Destination = Destination,
                DateFrom = DateFrom,
                DateTo = DateTo
            };

            tripRepository.Add(trip);

            OnNavigateToTripDetails?.Invoke();
            await NavigationService.Navigate<TripDetailsViewModel, Trip>(trip);
            Messenger.Publish(new NewTripCreatedMessage(this, trip));
        }
    }
}
