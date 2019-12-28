using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tripplanner.Business.Messages;
using Tripplanner.Business.Models;
using Tripplanner.Business.Repositories;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.ViewModels.Wrappers
{
    public class CurrencyPairViewModel : ViewModelBase
    {
        private ExchangeRate exchangeRate;
        private Trip trip;
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly ICurrencyService currencyService;

        public CurrencyPairViewModel(ExchangeRate exchangeRate, Trip trip, 
            IExchangeRateRepository exchangeRateRepository, ICurrencyService currencyService)
        {
            this.exchangeRate = exchangeRate;
            this.trip = trip;
            this.exchangeRateRepository = exchangeRateRepository;
            this.currencyService = currencyService;
            UpdateRateCommand = GetAsyncCommand(UpdateRate);
            RemoveCurrencyPairCommand = GetCommand(RemoveCurrencyPair);
        }

        public ICommand UpdateRateCommand { get; }
        public ICommand RemoveCurrencyPairCommand { get; }

        public ExchangeRate Rate => exchangeRate;

        private async Task UpdateRate()
        {
            var newRate = await currencyService.GetRate(exchangeRate.SourceCurrency, exchangeRate.TargetCurrency);
            exchangeRate.LastRate = newRate;
            exchangeRateRepository.AddOrUpdateSingle(exchangeRate);
            await RaisePropertyChanged(() => Rate);
        }

        private void RemoveCurrencyPair()
        {
            exchangeRateRepository.Delete(exchangeRate);
            PublishEvent(new CurrencyPairDeletedMessage(this));
        }
    }
}
