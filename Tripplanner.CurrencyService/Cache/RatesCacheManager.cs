using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tripplanner.CurrencyService.Cache
{
    public class RatesCacheManager : IRatesCacheManager
    {
        private const string CurrencyListUrl = "https://openexchangerates.org/api/currencies.json?app_id=8dcbc8e510ee419180282b56a70427b4";
        private const string ExchangeRatesUrl = "https://openexchangerates.org/api/latest.json?app_id=8dcbc8e510ee419180282b56a70427b4";
        private Dictionary<string, string> currencyNameMaps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, decimal> ratesAgainstUsd = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        private ReaderWriterLockSlim currencyListLock = new ReaderWriterLockSlim();
        private ReaderWriterLockSlim ratesLock = new ReaderWriterLockSlim();
        private int refreshRatesLock;
        private int refreshNamesLock;
        private HttpClient httpClient;

        public RatesCacheManager()
        {
            httpClient = new HttpClient();
            RefreshRates().Wait();
            RefreshNames().Wait();
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(new TimeSpan(1, 0, 0));
                    await RefreshRates();
                }
            });
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(new TimeSpan(1, 0, 0, 0));
                    await RefreshNames();
                }
            });
        }

        public Dictionary<string, string> SupportedCurrencies()
        {
            currencyListLock.EnterReadLock();
            try
            {
                return currencyNameMaps;
            }
            finally
            {
                currencyListLock.ExitReadLock();
            }
        }

        public decimal GetRate(string fromCcy, string toCcy)
        {
            decimal fromRate, toRate;
            ratesLock.EnterReadLock();
            try
            {
                if (!ratesAgainstUsd.ContainsKey(fromCcy))
                {
                    throw new ArgumentException("Source currency is not supported");
                }

                if (!ratesAgainstUsd.ContainsKey(toCcy))
                {
                    throw new ArgumentException("Target currency is not supported");
                }

                fromRate = ratesAgainstUsd[fromCcy];
                toRate = ratesAgainstUsd[toCcy];
            }
            finally
            {
                ratesLock.ExitReadLock();
            }

            if (fromRate <= 0)
            {
                throw new InvalidOperationException($"{fromCcy} rate against USD is invalid: {fromRate}.");
            }

            return toRate / fromRate;
        }

        private async Task RefreshRates()
        {
            if (Interlocked.CompareExchange(ref refreshRatesLock, 1, 0) == 0)
            {
                try
                {
                    var updatedRates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
                    var response = await httpClient.GetAsync(ExchangeRatesUrl);
                    var content = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject(content) as JObject;
                    foreach (JProperty property in json["rates"])
                    {
                        updatedRates.Add(property.Name, Decimal.Parse(property.Value.ToString()));
                    }
                    ratesLock.EnterWriteLock();
                    try
                    {
                        ratesAgainstUsd = updatedRates;
                    }
                    finally
                    {
                        ratesLock.ExitWriteLock();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Interlocked.Exchange(ref refreshRatesLock, 0);
                }
            }
        }

        private async Task RefreshNames()
        {
            if (Interlocked.CompareExchange(ref refreshNamesLock, 1, 0) == 0)
            {
                try
                {
                    var updatedNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    var response = await httpClient.GetAsync(CurrencyListUrl);
                    var content = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject(content) as JObject;
                    foreach (JProperty property in json.Properties())
                    {
                        updatedNames.Add(property.Name, property.Value.ToString());
                    }

                    currencyListLock.EnterWriteLock();
                    try
                    {
                        currencyNameMaps = updatedNames;
                    }
                    finally
                    {
                        currencyListLock.ExitWriteLock();
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    Interlocked.Exchange(ref refreshNamesLock, 0);
                }
            }
        }
    }
}
