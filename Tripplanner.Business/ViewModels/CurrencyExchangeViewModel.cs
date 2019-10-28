using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class CurrencyExchangeViewModel : TripAwareViewModelBase
    {
        private readonly ICurrencyService currencyService;

        public CurrencyExchangeViewModel(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
            AddNewCurrencyPairCommand = GetCommand(AddNewCurrencyPair);
            RefreshRatesCommand = GetAsyncCommand(async () => await RefreshRates());
            IndeterminateLoading = true;
        }

        public ICommand AddNewCurrencyPairCommand { get; }
        public ICommand RefreshRatesCommand { get; }

        private ObservableCollection<ExchangeRate> exchangeRates;

        public ObservableCollection<ExchangeRate> ExchangeRates
        {
            get => exchangeRates;
            set
            {
                exchangeRates = value;
                RaisePropertyChanged(() => ExchangeRates);
            }
        }

        public bool IndeterminateLoading { get; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        private void AddNewCurrencyPair()
        {
            NavigationService.Navigate<NewCurrencyPairViewModel>();
        }

        private async Task RefreshRates()
        {
            IsLoading = true;
            await RaisePropertyChanged(() => ExchangeRates);
            IsLoading = false;
        }
    }
}
