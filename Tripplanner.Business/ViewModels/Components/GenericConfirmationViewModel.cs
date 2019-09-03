using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.Configs;

namespace Tripplanner.Business.ViewModels.Components
{
    public class GenericConfirmationViewModel : ViewModelBase<Confirmation>, IDismissibleComponent
    {
        private Confirmation confirmation;

        public GenericConfirmationViewModel()
        {
            ProceedCommand = GetCommand(Proceed);
            CancelCommand = GetCommand(Cancel);
        }

        public override void Prepare(Confirmation parameter)
        {
            confirmation = parameter;
            ConfirmationMessage = confirmation.Message;
        }

        public ICommand ProceedCommand { get; }
        public ICommand CancelCommand { get; }
        public Action OnFinish { get; set; }

        public string ConfirmationMessage { get; set; }

        private void Proceed()
        {
            confirmation.OnProceed();
            OnFinish();
        }

        private void Cancel()
        {
            confirmation.OnCancel();
            OnFinish();
        }
    }
}
