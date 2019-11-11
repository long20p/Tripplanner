using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels
{
    public class CurrencyExchangeViewModel : TripAwareViewModelBase
    {
        private readonly ICurrencyService currencyService;
        private readonly IExchangeRateRepository exchangeRateRepository;

        public CurrencyExchangeViewModel(ICurrencyService currencyService, IExchangeRateRepository exchangeRateRepository)
        {
            this.currencyService = currencyService;
            this.exchangeRateRepository = exchangeRateRepository;
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
            await LoadExchangeRates();
            await base.Initialize();
        }

        private async Task LoadExchangeRates()
        {
            IsLoading = true;
            var all = await Task.Run(() => exchangeRateRepository.Where(x => x.TripId == Trip.UniqueId).ToList());
            ExchangeRates = new ObservableCollection<ExchangeRate>(all);
            IsLoading = false;
        }

        private void AddNewCurrencyPair()
        {
            Action<ExchangeRate> addExchangeRate = rate =>
            {
                rate.TripId = Trip.UniqueId;
                exchangeRateRepository.Add(rate);
                ExchangeRates.Add(rate);
                RaisePropertyChanged(() => ExchangeRates);
            };
            NavigationService.Navigate<NewCurrencyPairViewModel, Action<ExchangeRate>>(addExchangeRate);
        }

        private async Task RefreshRates()
        {
            IsLoading = true;
            await RaisePropertyChanged(() => ExchangeRates);
            IsLoading = false;
        }
    }
}
