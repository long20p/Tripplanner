using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;
using Tripplanner.Business.ViewModels.Wrappers;

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
            SubscribeToEvent<CurrencyPairDeletedMessage>(msg => RemoveCurrencyPair(msg.CurrencyPair));
        }

        public ICommand AddNewCurrencyPairCommand { get; }
        public ICommand RefreshRatesCommand { get; }

        private ObservableCollection<CurrencyPairViewModel> currencyPairs;

        public ObservableCollection<CurrencyPairViewModel> CurrencyPairs
        {
            get => currencyPairs;
            set
            {
                currencyPairs = value;
                RaisePropertyChanged(() => CurrencyPairs);
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
            var all = await Task.Run(() => exchangeRateRepository.Where(x => x.TripId == Trip.UniqueId));
            CurrencyPairs = new ObservableCollection<CurrencyPairViewModel>(all
                .Select(x => new CurrencyPairViewModel(x, Trip, exchangeRateRepository, currencyService)));
            IsLoading = false;
        }

        private void AddNewCurrencyPair()
        {
            Action<ExchangeRate> addExchangeRate = rate =>
            {
                rate.TripId = Trip.UniqueId;
                exchangeRateRepository.Add(rate);
                CurrencyPairs.Add(new CurrencyPairViewModel(rate, Trip, exchangeRateRepository, currencyService));
                RaisePropertyChanged(() => CurrencyPairs);
            };
            NavigationService.Navigate<NewCurrencyPairViewModel, Action<ExchangeRate>>(addExchangeRate);
        }

        private void RemoveCurrencyPair(CurrencyPairViewModel pair)
        {
            CurrencyPairs.Remove(pair);
            RaisePropertyChanged(() => CurrencyPairs);
        }

        private async Task RefreshRates()
        {
            IsLoading = true;
            await RaisePropertyChanged(() => CurrencyPairs);
            IsLoading = false;
        }
    }
}
