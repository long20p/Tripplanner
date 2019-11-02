using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tripplanner.CurrencyService.Cache
{
    public interface IRatesCacheManager
    {
        Dictionary<string, string> SupportedCurrencies();
        decimal GetRate(string fromCcy, string toCcy);
    }
}
