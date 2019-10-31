using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tripplanner.CurrencyService.Cache;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tripplanner.CurrencyService.Controllers
{
    [Route("api/[controller]")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IRatesCacheManager ratesCacheManager;

        public ExchangeRatesController(IRatesCacheManager ratesCacheManager)
        {
            this.ratesCacheManager = ratesCacheManager;
        }

        [HttpGet(nameof(CurrencyNames))]
        public Dictionary<string, string> CurrencyNames()
        {
            return ratesCacheManager.SupportedCurrencies();
        }

        [HttpGet]
        public ActionResult<decimal> Rates(string from, string to)
        {
            if (from == null || to == null)
            {
                return BadRequest("Either currency is missing or both");
            }

            return ratesCacheManager.GetRate(from, to);
        }
    }
}
