using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tripplanner.Business.ViewModels.Components
{
    public class DateSelectorViewModel : ViewModelBase<Action<DateTime>>, IDismissibleComponent
    {
        private Action<DateTime> setDateValue;

        public DateSelectorViewModel()
        {
            SelectDateCommand = GetAsyncCommand(async () => await SelectDate());
            SelectedDate = DateTime.Today;
        }

        public Action OnFinish { get; set; }
        public DateTime SelectedDate { get; set; }
        public ICommand SelectDateCommand { get; }

        public override void Prepare(Action<DateTime> parameter)
        {
            setDateValue = parameter;
        }

        private async Task SelectDate()
        {
            setDateValue(SelectedDate);
            OnFinish?.Invoke();
        }
    }
}
