using System;
using System.Collections.Generic;
using System.Text;

namespace Tripplanner.Business.Services
{
    public interface IDialogService
    {
        void ShowDialog(Action onProceed, Action onCancel);
    }
}
