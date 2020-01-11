using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class TransportEditParam
    {
        public LongDistanceTransport CurrentTransport { get; set; }
        public Action<LongDistanceTransport> EditAction { get; set; }
    }

    public class TransportEditViewModel : ViewModelBase<TransportEditParam>, IDismissibleComponent
    {
        private TransportEditParam editParam;

        public TransportEditViewModel()
        {
            TransportTypes = new List<string>(Enum.GetNames(typeof(LongDistanceTransportType)));
            MiddayIndicators = new List<string> { "", "AM", "PM" };
            EditCommand = GetCommand(Edit);
            CancelCommand = GetCommand(Cancel);
            OpenDepartureCalendarCommand = GetAsyncCommand(OpenDepartureCalendar);
            OpenArrivalCalendarCommand = GetAsyncCommand(OpenArrivalCalendar);
        }

        public ICommand EditCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand OpenDepartureCalendarCommand { get; }
        public ICommand OpenArrivalCalendarCommand { get; }

        public Action OnFinish { get; set; }

        public List<string> TransportTypes { get; }
        public List<string> MiddayIndicators { get; }

        private string selectedTransportType;
        public string SelectedTransportType
        {
            get => selectedTransportType;
            set 
            { 
                selectedTransportType = value;
                RaisePropertyChanged(() => SelectedTransportType);
            }
        }

        private string transportCompany;
        public string TransportCompany
        {
            get => transportCompany;
            set 
            { 
                transportCompany = value;
                RaisePropertyChanged(() => TransportCompany);
            }
        }

        private double distance;
        public double Distance
        {
            get => distance;
            set 
            { 
                distance = value;
                RaisePropertyChanged(() => Distance);
            }
        }

        private string startLocation;
        public string StartLocation
        {
            get => startLocation;
            set 
            { 
                startLocation = value;
                RaisePropertyChanged(() => StartLocation);
            }
        }

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

        #region Departure

        private DateTime departureDate;
        public DateTime DepartureDate
        {
            get => departureDate;
            set 
            {
                departureDate = value;
                RaisePropertyChanged(() => DepartureDate);
            }
        }

        private int departureTimeHour;
        public int DepartureTimeHour
        {
            get => departureTimeHour;
            set 
            { 
                departureTimeHour = value;
                RaisePropertyChanged(() => DepartureTimeHour);
            }
        }

        private int departureTimeMinute;
        public int DepartureTimeMinute
        {
            get => departureTimeMinute;
            set 
            { 
                departureTimeMinute = value;
                RaisePropertyChanged(() => DepartureTimeMinute);
            }
        }

        private string departureTimeMiddayIndicator;
        public string DepartureTimeMiddayIndicator
        {
            get => departureTimeMiddayIndicator;
            set 
            {
                departureTimeMiddayIndicator = value;
                RaisePropertyChanged(() => DepartureTimeMiddayIndicator);
            }
        }

        #endregion

        #region Arrival

        private DateTime arrivalDate;
        public DateTime ArrivalDate
        {
            get => arrivalDate;
            set
            {
                arrivalDate = value;
                RaisePropertyChanged(() => ArrivalDate);
            }
        }

        private int arrivalTimeHour;

        public int ArrivalTimeHour
        {
            get => arrivalTimeHour;
            set
            { 
                arrivalTimeHour = value;
                RaisePropertyChanged(() => ArrivalTimeHour);
            }
        }

        private int arrivalTimeMinute;

        public int ArrivalTimeMinute
        {
            get => arrivalTimeMinute;
            set 
            { 
                arrivalTimeMinute = value;
                RaisePropertyChanged(() => ArrivalTimeMinute);
            }
        }

        private int arrivalTimeMiddayIndicator;
        public int ArrivalTimeMiddayIndicator
        {
            get => arrivalTimeMiddayIndicator;
            set 
            { 
                arrivalTimeMiddayIndicator = value;
                RaisePropertyChanged(() => ArrivalTimeMiddayIndicator);
            }
        }

        #endregion

        private string ticketNumber;
        public string TicketNumber
        {
            get => ticketNumber;
            set 
            { 
                ticketNumber = value;
                RaisePropertyChanged(() => TicketNumber);
            }
        }

        private string additionalInfo;
        public string AdditionalInfo
        {
            get => additionalInfo;
            set 
            { 
                additionalInfo = value;
                RaisePropertyChanged(() => AdditionalInfo);
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

        public override void Prepare(TransportEditParam parameter)
        {
            editParam = parameter;
            if(editParam.CurrentTransport != null)
            {
                ActionName = "Update";
                SelectedTransportType = editParam.CurrentTransport.TransportType.ToString();
                TransportCompany = editParam.CurrentTransport.TransportCompany;
                Distance = editParam.CurrentTransport.Distance;
                StartLocation = editParam.CurrentTransport.StartLocation;
                Destination = editParam.CurrentTransport.EndLocation;
                DepartureDate = editParam.CurrentTransport.DepartureDate;
                ArrivalDate = editParam.CurrentTransport.ArrivalDate;
                TicketNumber = editParam.CurrentTransport.TicketNumber;
                AdditionalInfo = editParam.CurrentTransport.AdditionalInfo;
            }
            else
            {
                ActionName = "Add";
            }
        }

        private void Edit()
        {
            if(editParam.CurrentTransport == null)
            {
                editParam.CurrentTransport = new LongDistanceTransport { UniqueId = Guid.NewGuid() };
            }

            editParam.CurrentTransport.TransportType = (LongDistanceTransportType)Enum.Parse(typeof(LongDistanceTransportType), SelectedTransportType);
            editParam.CurrentTransport.TransportCompany = TransportCompany;
            editParam.CurrentTransport.Distance = Distance;
            editParam.CurrentTransport.StartLocation = StartLocation;
            editParam.CurrentTransport.EndLocation = Destination;
            editParam.CurrentTransport.DepartureDate = DepartureDate;
            editParam.CurrentTransport.ArrivalDate = ArrivalDate;
            editParam.CurrentTransport.TicketNumber = TicketNumber;
            editParam.CurrentTransport.AdditionalInfo = AdditionalInfo;

            editParam.EditAction(editParam.CurrentTransport);

            OnFinish();
        }

        private void Cancel()
        {
            OnFinish();
        }

        private async Task OpenDepartureCalendar()
        {
            await NavigationService.Navigate<DateSelectorViewModel, Action<DateTime>>(from => DepartureDate = from);
        }

        private async Task OpenArrivalCalendar()
        {
            await NavigationService.Navigate<DateSelectorViewModel, Action<DateTime>>(to => ArrivalDate = to);
        }
    }
}
