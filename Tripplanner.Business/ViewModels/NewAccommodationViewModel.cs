using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tripplanner.Business.ViewModels
{
    public class NewAccommodationViewModel : ViewModelBase
    {
        public NewAccommodationViewModel()
        {
            AddCommand = GetAsyncCommand(async () => await AddNewAccommodationEntry());
        }

        public ICommand AddCommand { get; }

        private async Task AddNewAccommodationEntry()
        {

        }
    }
}
