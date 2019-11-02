using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tripplanner.Business.Models;

namespace Tripplanner.Business.Services
{
    public interface ICurrencyService
    {
        List<Currency> Currencies { get; }
        Task<decimal> GetRate(string fromCcy, string toCcy);
    }
}
