using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private const string GetRateUrl = "https://tripplanner-currencyservice.azurewebsites.net/api/ExchangeRates/{0}/{1}";
        private const string CurrencyNamesUrl = "https://tripplanner-currencyservice.azurewebsites.net/api/ExchangeRates/CurrencyNames";

        private readonly HttpClient httpClient;

        public CurrencyService()
        {
            httpClient = new HttpClient();
            Currencies = GetCurrencies();
        }

        public List<Currency> Currencies { get; }

        public async Task<decimal> GetRate(string fromCcy, string toCcy)
        {
            var response = await httpClient.GetStringAsync(string.Format(GetRateUrl, fromCcy, toCcy));
            return decimal.TryParse(response, out var rate) ? rate : -1;
        }

        private List<Currency> GetCurrencies()
        {
            var updatedNames = new List<Currency>();
            try
            {
                var response = httpClient.GetStringAsync(CurrencyNamesUrl).Result;
                var json = JsonConvert.DeserializeObject(response) as JObject;
                foreach (JProperty property in json.Properties())
                {
                    updatedNames.Add(new Currency { Code = property.Name, Name = property.Value.ToString() });
                }
            }
            catch (Exception ex)
            {
                
            }

            return updatedNames;
        }
    }
}
