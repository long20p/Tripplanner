using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels.Components;

namespace Tripplanner.Business.ViewModels
{
    public class NewCurrencyPairViewModel : ViewModelBase<Action<ExchangeRate>>, IDismissibleComponent
    {
        private ICurrencyService currencyService;
        private Action<ExchangeRate> addExchangeRate;

        public NewCurrencyPairViewModel(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
            Currencies = new ObservableCollection<Currency>(currencyService.Currencies);
            AddCurrencyPairCommand = GetAsyncCommand(async () => await AddCurrencyPair());
            CancelCommand = GetCommand(Cancel);
            SelectedSourceCurrency = Currencies.FirstOrDefault(c => c.Code == "USD");
            SelectedTargetCurrency = Currencies.FirstOrDefault(c => c.Code == "EUR");
        }

        public ICommand AddCurrencyPairCommand { get; }
        public ICommand CancelCommand { get; }
        public Action OnFinish { get; set; }

        private ObservableCollection<Currency> _currencies;
        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                RaisePropertyChanged(() => Currencies);
            }
        }

        private string amount;
        public string Amount
        {
            get => amount;
            set
            {
                amount = value;
                RaisePropertyChanged(() => Amount);
            }

        }

        private Currency selectedSourceCurrency;
        public Currency SelectedSourceCurrency
        {
            get => selectedSourceCurrency;
            set
            {
                selectedSourceCurrency = value;
                RaisePropertyChanged(() => SelectedSourceCurrency);
            }

        }

        private Currency selectedTargetCurrency;
        public Currency SelectedTargetCurrency
        {
            get => selectedTargetCurrency;
            set
            {
                selectedTargetCurrency = value;
                RaisePropertyChanged(() => SelectedTargetCurrency);
            }
        }

        private async Task AddCurrencyPair()
        {
            var rateValue = await currencyService.GetRate(SelectedSourceCurrency.Code, SelectedTargetCurrency.Code);
            var exchangeRate = new ExchangeRate
            {
                UniqueId = Guid.NewGuid(),
                SourceCurrency = SelectedSourceCurrency.Code,
                TargetCurrency = SelectedTargetCurrency.Code,
                AmountInSource = decimal.Parse(Amount),
                LastRate = rateValue,
                LastRateValidAt = DateTime.Now
            };
            addExchangeRate(exchangeRate);
            OnFinish();
        }

        private void Cancel()
        {
            OnFinish();
        }

        public override void Prepare(Action<ExchangeRate> parameter)
        {
            addExchangeRate = parameter;
        }
    }
}
