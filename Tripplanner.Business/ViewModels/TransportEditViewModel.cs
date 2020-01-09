using System;
using System.Collections.Generic;
using System.Text;
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
            EditCommand = GetCommand(Edit);
            CancelCommand = GetCommand(Cancel);
        }

        public ICommand EditCommand { get; }
        public ICommand CancelCommand { get; }

        public Action OnFinish { get; set; }

        public List<string> TransportTypes { get; }

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

        private DateTime startTime;
        public DateTime StartTime
        {
            get => startTime;
            set 
            {
                startTime = value;
                RaisePropertyChanged(() => StartTime);
            }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get => endTime;
            set 
            {
                endTime = value;
                RaisePropertyChanged(() => EndTime);
            }
        }

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
                StartTime = editParam.CurrentTransport.StartTime;
                EndTime = editParam.CurrentTransport.EndTime;
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
            editParam.CurrentTransport.StartTime = StartTime;
            editParam.CurrentTransport.EndTime = EndTime;
            editParam.CurrentTransport.TicketNumber = TicketNumber;
            editParam.CurrentTransport.AdditionalInfo = AdditionalInfo;

            editParam.EditAction(editParam.CurrentTransport);

            OnFinish();
        }

        private void Cancel()
        {
            OnFinish();
        }
    }
}
