using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class NewAccommodationViewModel : ViewModelBase<Action<Accommodation>>, IDismissibleComponent
    {
        private Action<Accommodation> createNewAccommodationEntry;
        

        public NewAccommodationViewModel()
        {
            OpenFromCalendarCommand = GetAsyncCommand(async () => await OpenFromCalendar());
            OpenToCalendarCommand = GetAsyncCommand(async () => await OpenToCalendar());
            AddCommand = GetAsyncCommand(async () => await AddNewAccommodationEntry());
            CancelCommand = GetCommand(Cancel);
        }

        public Action OnFinish { get; set; }
        public ICommand OpenFromCalendarCommand { get; }
        public ICommand OpenToCalendarCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public string Address { get; set; }

        private DateTime? dateFrom;
        public DateTime? DateFrom
        {
            get => dateFrom;
            set
            {
                dateFrom = value;
                DateFromText = dateFrom != null ? dateFrom.Value.ToString("dd.MM.yyyy") : string.Empty;
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

        private DateTime? dateTo;
        public DateTime? DateTo
        {
            get => dateTo;
            set
            {
                dateTo = value;
                DateToText = dateTo != null ? dateTo.Value.ToString("dd.MM.yyyy") : string.Empty;
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

        private bool isIdenticalToTripDates;
        public bool IsIdenticalToTripDates
        {
            get => isIdenticalToTripDates;
            set
            {
                isIdenticalToTripDates = value;
                if (isIdenticalToTripDates)
                {
                    DateFrom = null;
                    DateTo = null;
                }
                RaisePropertyChanged(() => IsIdenticalToTripDates);
            }
        }

        public string Note { get; set; }

        private async Task AddNewAccommodationEntry()
        {
            if (!Validate())
            {
                return;
            }

            var accommodation = new Accommodation
            {
                UniqueId = Guid.NewGuid(),
                Address = Address,
                From = DateFrom,
                To = DateTo,
                UseTripDates = IsIdenticalToTripDates,
                Note = Note
            };

            createNewAccommodationEntry(accommodation);
            OnFinish();
        }

        private void Cancel()
        {
            OnFinish();
        }

        private bool Validate()
        {
            return !string.IsNullOrWhiteSpace(Address)
                   && (IsIdenticalToTripDates || (DateFrom.HasValue && DateTo.HasValue));
        }

        public override void Prepare(Action<Accommodation> parameter)
        {
            createNewAccommodationEntry = parameter;
        }

        private async Task OpenFromCalendar()
        {
            await NavigationService.Navigate<DateSelectorViewModel, Action<DateTime>>(from => DateFrom = from);
        }

        private async Task OpenToCalendar()
        {
            await NavigationService.Navigate<DateSelectorViewModel, Action<DateTime>>(to => DateTo = to);
        }
    }
}
