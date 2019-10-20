using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class NewCurrencyPairViewModel : ViewModelBase, IDismissibleComponent
    {
        public NewCurrencyPairViewModel()
        {
            Currencies = new ObservableCollection<string> { "USD", "EUR", "JPY", "GBP", "SGD" };
            SelectSourceCurrencyCommand = GetCommand(SelectSourceCurrency);
            SelectTargetCurrencyCommand = GetCommand(SelectTargetCurrency);
            AddCurrencyPairCommand = GetCommand(AddCurrencyPair);
            CancelCommand = GetCommand(Cancel);
        }

        public ICommand SelectSourceCurrencyCommand { get; }
        public ICommand SelectTargetCurrencyCommand { get; }
        public ICommand AddCurrencyPairCommand { get; }
        public ICommand CancelCommand { get; }
        public Action OnFinish { get; set; }

        private ObservableCollection<string> _currencies;
        public ObservableCollection<string> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                RaisePropertyChanged(() => Currencies);
            }
        }

        private void SelectSourceCurrency()
        {

        }

        private void SelectTargetCurrency()
        {

        }

        private void AddCurrencyPair()
        {

        }

        private void Cancel()
        {
            OnFinish();
        }
    }
}
