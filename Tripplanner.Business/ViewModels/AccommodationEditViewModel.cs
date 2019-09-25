using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class AccommodationEditParam
    {
        public Accommodation CurrentAccommodation { get; set; }
        public Action<Accommodation> EditAction { get; set; }
    }

    public class AccommodationEditViewModel : ViewModelBase<AccommodationEditParam>, IDismissibleComponent
    {
        private AccommodationEditParam accommodationEditArgument;
        

        public AccommodationEditViewModel()
        {
            OpenFromCalendarCommand = GetAsyncCommand(async () => await OpenFromCalendar());
            OpenToCalendarCommand = GetAsyncCommand(async () => await OpenToCalendar());
            EditCommand = GetCommand(EditAccommodationEntry);
            CancelCommand = GetCommand(Cancel);
        }

        public Action OnFinish { get; set; }
        public ICommand OpenFromCalendarCommand { get; }
        public ICommand OpenToCalendarCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand CancelCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        private string actionName;
        public string ActionName
        {
            get => actionName;
            set
            {
                actionName = value;
                RaisePropertyChanged(() => ActionName);
            }
        }

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

        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                RaisePropertyChanged(() => Note);
            }
        }

        private void EditAccommodationEntry()
        {
            if (!Validate())
            {
                return;
            }

            if (accommodationEditArgument.CurrentAccommodation == null)
            {
                accommodationEditArgument.CurrentAccommodation = new Accommodation {UniqueId = Guid.NewGuid()};
            }

            accommodationEditArgument.CurrentAccommodation.Name = Name;
            accommodationEditArgument.CurrentAccommodation.Address = Address;
            accommodationEditArgument.CurrentAccommodation.From = DateFrom;
            accommodationEditArgument.CurrentAccommodation.To = DateTo;
            accommodationEditArgument.CurrentAccommodation.UseTripDates = IsIdenticalToTripDates;
            accommodationEditArgument.CurrentAccommodation.Note = Note;

            accommodationEditArgument.EditAction(accommodationEditArgument.CurrentAccommodation);
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

        public override void Prepare(AccommodationEditParam parameter)
        {
            accommodationEditArgument = parameter;
            if (accommodationEditArgument.CurrentAccommodation != null)
            {
                ActionName = "Update";
                Name = accommodationEditArgument.CurrentAccommodation.Name;
                Address = accommodationEditArgument.CurrentAccommodation.Address;
                DateFrom = accommodationEditArgument.CurrentAccommodation.From;
                DateTo = accommodationEditArgument.CurrentAccommodation.To;
                IsIdenticalToTripDates = accommodationEditArgument.CurrentAccommodation.UseTripDates;
                Note = accommodationEditArgument.CurrentAccommodation.Note;
            }
            else
            {
                ActionName = "Add";
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
    }
}
