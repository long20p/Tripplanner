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
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IsIdenticalToTripDates { get; set; }
        public string Note { get; set; }

        private async Task AddNewAccommodationEntry()
        {
            var accommodation = new Accommodation
            {
                Address = Address,
                Note = Note
            };

            createNewAccommodationEntry(accommodation);
            OnFinish();
        }

        private void Cancel()
        {
            OnFinish();
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
