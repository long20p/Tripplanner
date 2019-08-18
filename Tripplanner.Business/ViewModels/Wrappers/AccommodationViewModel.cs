using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class AccommodationViewModel : ViewModelBase
    {
        private Accommodation accommodation;
        private Trip trip;
        private readonly IAccommodationRepository accommodationRepository;

        public AccommodationViewModel(Accommodation accommodation, Trip trip, IAccommodationRepository accommodationRepository)
        {
            this.accommodation = accommodation;
            this.trip = trip;
            this.accommodationRepository = accommodationRepository;
            DeleteAccommodationCommand = GetCommand(DeleteAccommodation);
        }

        public ICommand DeleteAccommodationCommand { get; }

        public Accommodation Accommodation => accommodation;

        public string Dates => accommodation.UseTripDates
            ? $"{trip.DateFrom.ToString(Constants.DateFormat)} - {trip.DateTo.ToString(Constants.DateFormat)}"
            : $"{accommodation.From.Value.ToString(Constants.DateFormat)} - {accommodation.To.Value.ToString(Constants.DateFormat)}";

        private void DeleteAccommodation()
        {
            accommodationRepository.Delete(accommodation);
            Messenger.Publish(new AccommodationDeletedMessage(this));
        }
    }
}
