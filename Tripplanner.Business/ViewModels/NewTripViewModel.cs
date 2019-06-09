using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;
using Tripplanner.Business.Utils;

namespace Tripplanner.Business.ViewModels
{
    public class NewTripViewModel : ViewModelBase
    {
        private IStorageService storageService;
        private ISerializer serializer;

        public NewTripViewModel(IStorageService storageService, ISerializer serializer)
        {
            this.storageService = storageService;
            this.serializer = serializer;
            CreateNewCommand = new MvxAsyncCommand(async () => await CreateNewTrip());
        }

        public MvxAsyncCommand CreateNewCommand { get; }
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

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        private async Task CreateNewTrip()
        {
            var tripInfo = new TripInfo
            {
                TripId = Guid.NewGuid().ToString(),
                Destination = Destination,
                DateFrom = DateFrom,
                DateTo = DateTo
            };

            var data = serializer.Serialize(tripInfo);
            storageService.SaveTextFile(Path.Combine(tripInfo.TripId, Constants.GeneralTripInfoFile), data);

            OnNavigateToTripDetails?.Invoke();
            await NavigationService.Navigate<TripDetailsViewModel>();
            Messenger.Publish(new NewTripCreatedMessage(this, Destination));
        }
    }
}
