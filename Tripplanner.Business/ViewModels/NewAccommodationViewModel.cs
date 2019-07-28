using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class NewAccommodationViewModel : ViewModelBase, IDismissibleComponent
    {
        public NewAccommodationViewModel()
        {
            AddCommand = GetAsyncCommand(async () => await AddNewAccommodationEntry());
        }

        public Action OnFinish { get; set; }
        public ICommand AddCommand { get; }

        private async Task AddNewAccommodationEntry()
        {

        }
    }
}
